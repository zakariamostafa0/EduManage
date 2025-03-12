
using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        #endregion
        #region Handle Methods
        public async Task<string> AddRoleAsync(string roleName)
        {

            var identityRole = new IdentityRole();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (!result.Succeeded)
                return "Failed";
            return "Success";

        }

        public async Task<bool> IsRoleNameExistAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);

            //var role = await _roleManager.FindByNameAsync(roleName);
            //if (role != null)
            //    return true;
            //return false;
        }
        #endregion

    }
}
