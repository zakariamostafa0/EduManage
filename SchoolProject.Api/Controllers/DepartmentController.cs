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
    }
}
