using SchoolProject.Core.Features.Authentication.Commands.Models;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.Login)]
        public async Task<IActionResult> LogIn([FromForm] LoginCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
