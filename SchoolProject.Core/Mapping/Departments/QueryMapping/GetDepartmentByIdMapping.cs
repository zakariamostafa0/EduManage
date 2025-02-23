using SchoolProject.Core.Features.Department.Queries.Results;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {

            CreateMap<Department, GetDepartmentByIdResponse>()
                 .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DID))
                 .ForMember(dest => dest.DName, opt => opt.MapFrom(src => src.DName))
                 .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor.Name))
                 //.ForMember(dest => dest.StudentList, opt => opt.MapFrom(src => src.Students))
                 .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DeptSubjects))
                 .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors));

            //CreateMap<Student, StudentResponse>()
            //   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
            //   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<DepartmentSubject, SubjectsResponse>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubId))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.SubjectName));

            CreateMap<Instructor, InstructorResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        }
    }
}
