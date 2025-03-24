using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Service.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly ApplicationDBContext _dbContext;
        private readonly IUrlHelper _urlHelper;
        #endregion

        #region Constructors
        public ApplicationUserService(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            ApplicationDBContext dbContext,
            IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _dbContext = dbContext;
            _urlHelper = urlHelper;
        }

        #endregion

        #region Handles Methods

        public async Task<string> AddUserAsync(ApplicationUser user, string password)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {

                //if email exists?
                var email = await _userManager.FindByEmailAsync(user.Email);
                if (email != null)
                    return "EmailExist";
                //if user name exist?
                var username = await _userManager.FindByNameAsync(user.UserName);
                if (username != null)
                    return "UserNameExist";
                //created?
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                    return string.Join(", ", result.Errors.Select(e => e.Description).ToList());



                await _userManager.AddToRoleAsync(user, "User");

                //send confirm email
                SendEmailConfirmation(user);

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToCreateUser";
            }
        }
        public async Task<string> SendEmailConfirmationAgain(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
                return "EmailNotExist";
            if (user.EmailConfirmed)
                return "EmailAlreadyConfirmed";
            SendEmailConfirmation(user);
            return "Success";
        }

        private async Task<string> SendEmailConfirmation(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var requsetAccessor = _httpContextAccessor.HttpContext.Request;
            var returnURL = requsetAccessor.Scheme + "://" + requsetAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });

            //var returnURL = requsetAccessor.Scheme + "://" + requsetAccessor.Host + $"/Api/V1/Authentication/ConfirmEmail?UserId={user.Id}&Code={code}";

            string messageBody = $@"
                        <p>Please click on the link below to activate your email:</p>
                        <p><a href='{returnURL}'>Activate Email</a></p>
                        <p>Thank you</p>";
            return await _emailService.SendEmailAsync("zeko10199@gmail.com", messageBody, "Email Confirmation");

        }

        public async Task<string> SendResetPasswordEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return ("UserNotFound");

            // Generate password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Build the reset URL
            var request = _httpContextAccessor.HttpContext.Request;
            var resetUrl = $"{request.Scheme}://{request.Host}/api/Account/resetassword?userId={user.Id}&token={token}";

            // Send email
            string message = resetUrl;

            await _emailService.SendEmailAsync("zeko10199@gmail.com", message, "Reset Password");
            return "Success";
        }
        public async Task<string> ResetPassword(string userId, string token, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ("Invaliduser");

            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded)
                return (string.Join(", ", result.Errors.Select(e => e.Description)));

            return "Success";
        }
        #endregion
    }
}
