using SchoolProject.Core.Features.Authorization.Queries.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleResult>>
    {
        public string Id { get; set; }
        public GetRoleByIdQuery(string id)
        {
            Id = id;
        }
    }
}
