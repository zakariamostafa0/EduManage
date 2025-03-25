using SchoolProject.Core.Features.Department.Queries.Results;

namespace SchoolProject.Core.Features.Department.Queries.Models
{
    public class GetDepartmentStudentsCountByIdQuery : IRequest<Response<GetDepartmentStudentsCountByIdResult>>
    {
        public int DID { get; set; }
        public GetDepartmentStudentsCountByIdQuery(int dID)
        {
            DID = dID;
        }
    }
}
