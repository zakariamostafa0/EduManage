namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class StudentController : AppControllerBase
    {
        
        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentsListAsync()
        {
            var students = await Mediator.Send(new GetStudentListQuery());
            return Ok(students);
        }

        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentsByIdAsync([FromRoute]int id)
        {
            var student = await Mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(student);
        }

        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> CreateStudentAsyns([FromBody]AddStudentCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
