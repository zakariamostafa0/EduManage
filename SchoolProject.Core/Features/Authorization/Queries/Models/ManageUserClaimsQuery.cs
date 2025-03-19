namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResult>>
    {
        public string UserId { get; set; }
        public ManageUserClaimsQuery(string userId)
        {
            UserId = userId;
        }
    }
}
