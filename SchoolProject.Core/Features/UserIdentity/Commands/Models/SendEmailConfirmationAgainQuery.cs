namespace SchoolProject.Core.Features.UserIdentity.Commands.Models
{
    public class SendEmailConfirmationAgainQuery : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
