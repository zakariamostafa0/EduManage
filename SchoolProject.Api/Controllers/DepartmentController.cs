using SchoolProject.Core.Features.Department.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetById)]
        public async Task<IActionResult> GetDepartmentByIdAsync([FromRoute] int id)
        {
            var student = await Mediator.Send(new GetDepartmentByIdQuery(id));
            return NewResult(student);
        }
    }
}
