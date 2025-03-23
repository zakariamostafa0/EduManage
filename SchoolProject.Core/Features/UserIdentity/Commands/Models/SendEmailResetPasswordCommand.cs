namespace SchoolProject.Core.Features.UserIdentity.Commands.Models
{
    public class SendEmailResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
