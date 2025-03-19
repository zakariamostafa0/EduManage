namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserClaimsCommand : ManageUserClaimsResult, IRequest<Response<string>>
    {
    }
}
