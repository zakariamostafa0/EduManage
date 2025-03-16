using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken; // map works in concurrent enviroment
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        #endregion
        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
                                    IRefreshTokenRepository refreshTokenRepository,
                                    UserManager<ApplicationUser> userManager)
        {
            _jwtSettings = jwtSettings;
            //_userRefreshToken = userRefreshToken;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
        }
        #endregion

        #region Handles Methods
        public async Task<JwtAuthResult> GetJWTToken(ApplicationUser user)
        {
            var (jwtSecurityToken, accessToken) = await GenerateJWTToken(user);
            var refreshToken = GetRefreshToken(user.UserName);

            //save refresh token to the database
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = false,
                IsRevoked = false,
                JwtId = jwtSecurityToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);
            return new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
        private RefreshToken GetRefreshToken(string UserName)
        {
            var refreshToken = new RefreshToken()
            {
                TokenString = GenerateRefreshToken(),
                UserName = UserName,
                ExpiresOn = DateTime.Now.AddMinutes(_jwtSettings.RefreshTokenExpireDate),
            };
            //_userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private async Task<List<Claim>> GetClaimsAsync(ApplicationUser user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(UserClaimsModel.Id), user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(nameof(UserClaimsModel.UserName), user.UserName),
                new Claim(nameof(UserClaimsModel.PhoneNumber), user.PhoneNumber),
            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            return claims;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await GetClaimsAsync(user, roles.ToList());

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtSecurityToken = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                                             audience: _jwtSettings.Audience,
                                             claims: claims,
                                             notBefore: DateTime.UtcNow,
                                             expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpireDate),
                                             signingCredentials: signingCredentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return (jwtSecurityToken, accessToken);
        }

        public async Task<JwtAuthResult> GetRefreshToken(ApplicationUser user, JwtSecurityToken token, DateTime? expiryDate, string refreshToken)
        {
            //Generate Refresh Token
            var (jwtSecurityToken, NewToken) = await GenerateJWTToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = NewToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpiresOn = (DateTime)expiryDate; // Converting from DateTime? to DataTime
            var username = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.UserName)).Value;
            refreshTokenResult.UserName = username;
            response.RefreshToken = refreshTokenResult;
            return response;
        }
        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken token, string accessToken, string refreshToken)
        {
            if (token == null || !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                return ("AlgorithmIsWrong", null);

            if (token.ValidTo > DateTime.UtcNow)
                return ("TokenIsNotExpired", null);

            //Get User
            var userId = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                                    .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                         x.RefreshToken == refreshToken &&
                                                         x.UserId == userId);
            if (userRefreshToken == null)
                return ("RefreshTokenIsNotFound", null);

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expiryDate = userRefreshToken.ExpiryDate;
            return (userId, expiryDate);
        }

        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }
        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (SecurityTokenExpiredException)
            {
                return "Expired"; // Token is expired
            }
            catch (SecurityTokenValidationException)
            {
                return "InvalidToken"; // Token is invalid (signature, issuer, etc.)
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}"; // Other unexpected errors
            }
        }




        //private async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken)
        //{
        //    if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
        //    {
        //        return ("AlgorithmIsWrong", null);
        //    }
        //    if (jwtToken.ValidTo > DateTime.UtcNow)
        //    {
        //        return ("TokenIsNotExpired", null);
        //    }

        //    //Get User
        //    var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.Id)).Value;
        //    var userRefreshToken = await _unitOfWork.RefreshToken.GetTableNoTracking()
        //                                     .FirstOrDefaultAsync(x => x.Token == AccessToken
        //                                     && x.RefreshToken == RefreshToken &&
        //                                     x.UserId == int.Parse(userId));
        //    if (userRefreshToken == null)
        //    {
        //        return ("RefreshTokenIsNotFound", null);
        //    }

        //    if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
        //    {
        //        userRefreshToken.IsRevoked = true;
        //        userRefreshToken.IsUsed = false;
        //        await _unitOfWork.RefreshToken.UpdateAsync(userRefreshToken);
        //        return ("RefreshTokenIsExpired", null);
        //    }
        //    var ExpireDate = userRefreshToken.ExpiryDate;
        //    return (userId, ExpireDate);
        //}
        #endregion
    }
}
