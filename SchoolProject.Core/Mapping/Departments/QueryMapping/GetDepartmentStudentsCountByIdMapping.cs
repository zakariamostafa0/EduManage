using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Data.Entities.Procedure;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentStudentsCountByIdMapping()
        {
            //Procedures

            CreateMap<DepartmentStudentsCountProc, GetDepartmentStudentsCountByIdResult>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DName))
             .ForMember(dest => dest.StudentsCount, opt => opt.MapFrom(src => src.StudentsCount));
        }

    }
}
