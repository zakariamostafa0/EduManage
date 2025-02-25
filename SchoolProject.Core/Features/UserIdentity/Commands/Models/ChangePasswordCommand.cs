namespace SchoolProject.Core.Features.UserIdentity.Commands.Models
{
    public class ChangePasswordCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
