using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        public string UserId { get; set; }
        public ManageUserRolesQuery(string userId)
        {
            UserId = userId;
        }
    }
}
