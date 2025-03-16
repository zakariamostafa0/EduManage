namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class UpdateRoleCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
    }
}
