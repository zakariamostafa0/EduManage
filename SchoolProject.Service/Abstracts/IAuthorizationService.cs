using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<string> EditRoleAsync(string roleName, string id);
        public Task<string> DeleteRoleAsync(string id);
        public Task<List<ApplicationRole>> GetAllRolesAsync();
        public Task<ApplicationRole> GetRoleById(string id);
        public Task<bool> IsRoleNameExistAsync(string roleName, string? id);
        public Task<bool> IsRoleExist(string id);
        public Task<ManageUserRolesResult> GetRolesForUserAsync(string userId);
        public Task<string> UpdateUserRolesAsync(ManageUserRolesResult request);
        public Task<ManageUserClaimsResult> GetUserClaimsAsync(string userId);
        public Task<string> UpdateUserClaimsAsync(ManageUserClaimsResult request);
    }
}
