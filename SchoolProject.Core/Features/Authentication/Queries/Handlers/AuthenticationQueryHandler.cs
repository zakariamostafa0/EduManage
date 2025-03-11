using SchoolProject.Core.Features.Authentication.Queries.Models;

namespace SchoolProject.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
                                                IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Construcors
        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> localizer,
                                          IAuthenticationService authenticationService) : base(localizer)
        {
            _localizer = localizer;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Handle Methods
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "Expired")
                return BadRequest<string>(result);
            return Success(result);

        }
        #endregion

    }
}
