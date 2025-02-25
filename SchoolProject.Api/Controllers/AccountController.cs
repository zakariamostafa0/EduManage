using SchoolProject.Core.Features.UserIdentity.Commands.Models;
using SchoolProject.Core.Features.UserIdentity.Queries.Models;

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
        [HttpPut(Router.AccountRouting.Edit)]
        public async Task<IActionResult> UpdateUserAsyns([FromBody] UpdateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.AccountRouting.Paginate)]
        public async Task<IActionResult> GetUserPaginationAsyns([FromQuery] GetUsersPaginationQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(Router.AccountRouting.GetUser)]
        public async Task<IActionResult> GetUser([FromQuery] GetUserByUsernameOrEmailQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
        [HttpDelete(Router.AccountRouting.Delete)]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var response = await Mediator.Send(new DeleteUserCommand(id));
            return NewResult(response);
        }
    }
}
