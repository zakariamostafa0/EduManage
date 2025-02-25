using SchoolProject.Core.Features.UserIdentity.Queries.Results;

namespace SchoolProject.Core.Features.UserIdentity.Queries.Models
{
    public class GetUserByUsernameOrEmailQuery : IRequest<Response<GetUserByUsernameOrEmailResponse>>
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
