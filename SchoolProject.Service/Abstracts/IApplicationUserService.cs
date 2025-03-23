using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstracts
{
    public interface IApplicationUserService
    {
        public Task<string> AddUserAsync(ApplicationUser user, string password);
        public Task<string> SendEmailConfirmationAgain(string userEmail);
    }
}
