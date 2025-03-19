using Microsoft.AspNetCore.Authorization;
using SchoolProject.Core.Features.Students.Commands.Models;
namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StudentController : AppControllerBase
    {
        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentsListAsync()
        {
            var students = await Mediator.Send(new GetStudentListQuery());
            return Ok(students);
        }
        [HttpGet(Router.StudentRouting.Paginate)]
        public async Task<IActionResult> GetStudentsListAsync([FromQuery] GetStudentPaginatedListQuery query)
        {
            var students = await Mediator.Send(query);
            return Ok(students);
        }

        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentsByIdAsync([FromRoute] int id)
        {
            var student = await Mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(student);
        }

        [Authorize(policy: "CreateStudent")]
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> CreateStudentAsyns([FromBody] AddStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(policy: "UpdateStudent")]
        [HttpPut(Router.StudentRouting.Update)]
        public async Task<IActionResult> UpdateStudentAsyns([FromBody] EditStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(policy: "DeleteStudent")]
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> DeleteStudentAsyns([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteStudentCommand(id));
            return NewResult(response);
        }
    }
}
