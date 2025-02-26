using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken; // map works in concurrent enviroment
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        #endregion
        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
                                    ConcurrentDictionary<string, RefreshToken> userRefreshToken,
                                    IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings;
            _userRefreshToken = userRefreshToken;
            _refreshTokenRepository = refreshTokenRepository;
        }
        #endregion

        #region Handles Methods
        public async Task<JwtAuthResult> GenerateJwtToken(ApplicationUser user)
        {

            var claims = GetClaims(user);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                                             audience: _jwtSettings.Audience,
                                             claims: claims,
                                             notBefore: null,
                                             expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpireDate),
                                             signingCredentials: signingCredentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
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
            _userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private List<Claim> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(UserClaimsModel.Id), user.Id),
                new Claim(nameof(UserClaimsModel.UserName), user.UserName),
                new Claim(nameof(UserClaimsModel.Email), user.Email),
                new Claim(nameof(UserClaimsModel.PhoneNumber), user.PhoneNumber),
            };
            return claims;
        }
        #endregion
    }
}
