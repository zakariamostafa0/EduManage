using Microsoft.AspNetCore.Authorization;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Features.Instructors.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = nameof(UserRoles.Admin))]
    public class InstructorController : AppControllerBase
    {
        [HttpGet(Router.InstructorRouting.GetSalarySummation)]
        public async Task<IActionResult> GetSalarySummation()
        {
            var students = await Mediator.Send(new GetInstructorsSalarySummationQuery());
            return Ok(students);
        }

        [HttpPost(Router.InstructorRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddInstructorCommand command)
        {
            var students = await Mediator.Send(command);
            return Ok(students);
        }
    }
}
