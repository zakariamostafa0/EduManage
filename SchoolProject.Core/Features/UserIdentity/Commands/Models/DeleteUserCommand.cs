namespace SchoolProject.Core.Features.UserIdentity.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
        public DeleteUserCommand(string id)
        {
            Id = id;
        }
    }
}
