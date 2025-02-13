using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void AddStudentCommandMapping()
        {
            CreateMap<AddStudentCommand, Student>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId));
            CreateMap<Student, AddStudentCommand>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DID));
        }
    }
}
