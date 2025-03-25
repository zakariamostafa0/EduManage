using SchoolProject.Core.Features.Department.Queries.Results;

namespace SchoolProject.Core.Features.Department.Queries.Models
{
    public class GetDepartmentsStudentsCountQuery : IRequest<Response<List<GetDepartmentsStudentsCountResult>>>
    {
    }
}
