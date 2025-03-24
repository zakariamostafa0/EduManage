using SchoolProject.Core.Features.Department.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetById)]
        public async Task<IActionResult> GetDepartmentByIdAsync([FromQuery] GetDepartmentByIdQuery query)
        {
            var student = await Mediator.Send(query);
            return NewResult(student);
        }
        [HttpGet(Router.DepartmentRouting.GetDepartmentsStudentsCount)]
        public async Task<IActionResult> GetDepartmentsStudentsCount()
        {
            var student = await Mediator.Send(new GetDepartmentsStudentsCountQuery());
            return NewResult(student);
        }
    }
}
