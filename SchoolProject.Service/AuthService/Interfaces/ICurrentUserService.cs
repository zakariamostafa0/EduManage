using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.AuthService.Interfaces
{
    public interface ICurrentUserService
    {
        public Task<ApplicationUser> CurrentUserAsync();
        public Task<List<string>> GetCurrentUserRolesAsync();
        public string GetUserId();
    }
}
