using SchoolProject.Core.Features.UserIdentity.Commands.Models;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AccountController : AppControllerBase
    {
        [HttpPost(Router.AccountRouting.Create)]
        public async Task<IActionResult> CreateUserAsyns([FromBody] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}
