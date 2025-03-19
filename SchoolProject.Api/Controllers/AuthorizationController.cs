using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    //[Authorize]
    public class AuthorizationController : AppControllerBase
    {
        [HttpGet(Router.AuthorizationRouting.GetAllRoles)]
        public async Task<IActionResult> GetAllRoleAsyns()
        {
            var response = await Mediator.Send(new GetRolesListQuery());
            return NewResult(response);
        }
        [HttpGet(Router.AuthorizationRouting.GetRoleById)]
        public async Task<IActionResult> GetRoleByIdAsyns([FromRoute] string id)
        {
            var response = await Mediator.Send(new GetRoleByIdQuery(id));
            return NewResult(response);
        }

        [HttpPost(Router.AuthorizationRouting.CreateRole)]
        public async Task<IActionResult> CreateRoleAsyns([FromBody] AddRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.UpdateRole)]
        public async Task<IActionResult> UpdateRoleAsyns([FromBody] UpdateRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete(Router.AuthorizationRouting.DeleteRole)]
        public async Task<IActionResult> DeleteRoleAsyns([FromRoute] string id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }
        [HttpGet(Router.AuthorizationRouting.GetUserRoles)]
        public async Task<IActionResult> GetUserRoles([FromRoute] string userId)
        {
            var response = await Mediator.Send(new ManageUserRolesQuery(userId));
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand request)
        {
            var response = await Mediator.Send(request);
            return NewResult(response);
        }

        [HttpGet(Router.AuthorizationRouting.GetUserClaims)]
        public async Task<IActionResult> GetUserClaims([FromRoute] string userId)
        {
            var response = await Mediator.Send(new ManageUserClaimsQuery(userId));
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand request)
        {
            var response = await Mediator.Send(request);
            return NewResult(response);
        }
    }
}
