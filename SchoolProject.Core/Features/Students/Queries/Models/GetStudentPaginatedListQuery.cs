namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedListQuery : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string[]? OrederBy { get; set; }
        public string? Search { get; set; }
    }
}
