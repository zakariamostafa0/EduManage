using Microsoft.AspNetCore.Http;

namespace SchoolProject.Core.Features.Instructors.Commands.Models
{
    public class AddInstructorCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
        public IFormFile? Image { get; set; }
        public string? SupervisorId { get; set; }
        public int DID { get; set; }

    }
}
