using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> CreateRoleAsyns([FromBody] AddRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
