using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.Id));

            CreateMap<Student, EditStudentCommand>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID));
        }
    }
}
