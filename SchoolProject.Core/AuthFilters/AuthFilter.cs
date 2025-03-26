using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolProject.Service.AuthService.Interfaces;

namespace SchoolProject.Core.AuthFilters
{
    public class AuthFilter : IAsyncActionFilter
    {
        public readonly ICurrentUserService _currentUserService;

        public AuthFilter(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var roles = await _currentUserService.GetCurrentUserRolesAsync();
                if (roles.All(x => x != nameof(UserRoles.Admin)))
                {
                    context.Result = new ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }
                else
                    await next();
            }
        }
    }
}
