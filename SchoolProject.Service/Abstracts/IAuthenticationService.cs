using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(ApplicationUser user);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtSecurityToken, string accessToken, string refreshToken);
        public Task<JwtAuthResult> GetRefreshToken(ApplicationUser user, JwtSecurityToken token, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateToken(string accessToken);

    }
}
