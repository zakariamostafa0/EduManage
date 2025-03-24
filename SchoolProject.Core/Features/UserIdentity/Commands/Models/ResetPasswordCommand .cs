namespace SchoolProject.Core.Features.UserIdentity.Commands.Models
{

    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
