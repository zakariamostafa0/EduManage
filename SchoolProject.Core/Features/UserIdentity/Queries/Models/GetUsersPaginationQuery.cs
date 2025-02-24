using SchoolProject.Core.Features.UserIdentity.Queries.Results;

namespace SchoolProject.Core.Features.UserIdentity.Queries.Models
{
    public class GetUsersPaginationQuery : IRequest<PaginatedResult<GetUsersResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
