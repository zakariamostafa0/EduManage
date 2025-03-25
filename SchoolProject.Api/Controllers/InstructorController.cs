using SchoolProject.Core.Features.Instructor.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpGet(Router.InstructorRouting.GetSalarySummation)]
        public async Task<IActionResult> GetSalarySummation()
        {
            var students = await Mediator.Send(new GetInstructorsSalarySummationQuery());
            return Ok(students);
        }
    }
}
