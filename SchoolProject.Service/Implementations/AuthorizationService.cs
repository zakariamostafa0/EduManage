
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Data;
using System.Security.Claims;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDBContext _dbContext;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDBContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        #endregion
        #region Handle Methods
        public async Task<string> AddRoleAsync(string roleName)
        {

            var identityRole = new ApplicationRole();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (!result.Succeeded)
                return "Failed";
            return "Success";

        }

        public async Task<string> EditRoleAsync(string roleName, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return "NotFound";
            role.Name = roleName;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return errors;
            }
            return "Success";
        }
        public async Task<string> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return "NotFound";
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users is not null && users.Count() > 0)
                return "There are users with this role";
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return errors;
            }
            return "Success";
        }
        public async Task<bool> IsRoleNameExistAsync(string roleName, string? roleId = null)
        {
            var query = _roleManager.Roles.Where(r => r.Name == roleName);

            if (roleId is not null)
            {
                query = query.Where(r => r.Id != roleId);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> IsRoleExist(string id)
        {
            return await _roleManager.RoleExistsAsync(id);
        }

        public async Task<List<ApplicationRole>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<ApplicationRole> GetRoleById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<ManageUserRolesResult> GetRolesForUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ManageUserRolesResult
                {
                    Success = false,
                };
            }

            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = await _roleManager.Roles.ToListAsync();

            var roles = new List<Roles>();
            foreach (var item in userRoles)
            {
                var role = new Roles();
                role.Id = item.Id;
                role.Name = item.Name;
                if (userRoleNames.Contains(item.Name))
                    role.HasRole = true;
                roles.Add(role);
            }
            var userRolesResult = new ManageUserRolesResult
            {
                UserId = user.Id,
                Success = true,
                Roles = roles
            };
            return userRolesResult;
        }

        public async Task<string> UpdateUserRolesAsync(ManageUserRolesResult request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //get user Old Roles
                var userRoles = await _userManager.GetRolesAsync(user);
                //Delete OldRoles
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    return "FailedToRemoveOldRoles";
                var selectedRoles = request.Roles.Where(x => x.HasRole == true).Select(x => x.Name);

                //Add the Roles HasRole=True
                var addRolesresult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addRolesresult.Succeeded)
                    return "FailedToAddNewRoles";
                await transact.CommitAsync();
                //return Result
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
        }
        public async Task<ManageUserClaimsResult> GetUserClaimsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ManageUserClaimsResult
                {
                    Success = false,
                };
            }

            var allUserClaims = await _userManager.GetClaimsAsync(user);
            //var userRoles = await _roleManager.Roles.ToListAsync();

            var userClaimList = new List<UserClaims>();
            foreach (var item in ClaimStore.claims)
            {
                var userClaim = new UserClaims();
                userClaim.Type = item.Type;
                if (allUserClaims.Any(c => c.Type == item.Type))
                    userClaim.Value = true;
                userClaimList.Add(userClaim);
            }
            var userRolesResult = new ManageUserClaimsResult
            {
                UserId = user.Id,
                Success = true,
                UserClaims = userClaimList
            };
            return userRolesResult;
        }
        public async Task<string> UpdateUserClaimsAsync(ManageUserClaimsResult request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                var userClaims = await _userManager.GetClaimsAsync(user);

                var removeResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeResult.Succeeded)
                    return "FailedToRemoveOldClaims";
                var selectedClaims = request.UserClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));

                var addClaimsresult = await _userManager.AddClaimsAsync(user, selectedClaims);
                if (!addClaimsresult.Succeeded)
                    return "FailedToAddNewClaims";

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserClaims";
            }
        }
        #endregion

    }
}
