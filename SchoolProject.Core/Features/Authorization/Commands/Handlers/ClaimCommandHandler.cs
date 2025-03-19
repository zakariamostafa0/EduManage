using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    internal class ClaimCommandHandler : ResponseHandler,
                                    IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Construcors
        public ClaimCommandHandler(IStringLocalizer<SharedResources> localizer,
                                    IAuthorizationService authorizationService) : base(localizer)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
        }


        #endregion

        #region Handle Methods
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaimsAsync(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_localizer[SharedResourcesKeys.UserNotFound]);
                case "FailedToRemoveOldClaims": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
                case "FailedToAddNewClaims": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToAddNewRoles]);
                case "FailedToUpdateUserClaims": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
            }
            return Success<string>(_localizer[SharedResourcesKeys.Success]);
        }
        #endregion
    }
}

