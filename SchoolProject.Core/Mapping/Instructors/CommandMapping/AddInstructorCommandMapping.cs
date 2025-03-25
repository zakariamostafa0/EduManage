using SchoolProject.Core.Features.Instructors.Commands.Models;

namespace SchoolProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile : Profile
    {
        public void AddInstructorCommandMapping()
        {
            CreateMap<AddInstructorCommand, Instructor>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            //CreateMap<Instructor, AddInstructorCommand>()
            //    .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DID));
        }
    }
}
