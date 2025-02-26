using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        #endregion
        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        #endregion

        #region Handles Methods
        public Task<string> GenerateJwtToken(ApplicationUser user)
        {

            var claims = new List<Claim>
            {
                new Claim(nameof(UserClaimsModel.Id), user.Id),
                new Claim(nameof(UserClaimsModel.UserName), user.UserName),
                new Claim(nameof(UserClaimsModel.Email), user.Email),
                new Claim(nameof(UserClaimsModel.PhoneNumber), user.PhoneNumber),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                                             audience: _jwtSettings.Audience,
                                             claims: claims,
                                             notBefore: null,
                                             expires: DateTime.UtcNow.AddDays(30),
                                             signingCredentials: signingCredentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Task.FromResult(token);
        }
        #endregion
    }
}
