using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.AuthService.Interfaces;

namespace SchoolProject.Service.AuthService.Implementaions
{
    public class CurrentUserService : ICurrentUserService
    {
        #region Fields
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Constructors
        public CurrentUserService(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> CurrentUserAsync()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new UnauthorizedAccessException();
            return user;
        }

        public async Task<List<string>> GetCurrentUserRolesAsync()
        {
            var user = await CurrentUserAsync();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public string GetUserId()
        {
            var userId = _contextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == nameof(UserClaimsModel.Id)).Value;
            if (userId == null)
                throw new UnauthorizedAccessException();
            return userId;
        }

        #endregion

        #region Handles Methods
        #endregion
    }
}
