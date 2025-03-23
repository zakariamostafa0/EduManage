using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstracts
{
    public interface IApplicationUserService
    {
        public Task<string> AddUserAsync(ApplicationUser user, string password);
        public Task<string> SendEmailConfirmationAgain(string userEmail);
        public Task<string> ResetPassword(string userId, string token, string password);
        public Task<string> SendResetPasswordEmail(string email);

    }
}
