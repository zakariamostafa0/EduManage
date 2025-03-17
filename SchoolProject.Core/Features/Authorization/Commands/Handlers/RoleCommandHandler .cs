using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
                                    IRequestHandler<AddRoleCommand, Response<string>>,
                                    IRequestHandler<UpdateRoleCommand, Response<string>>,
                                    IRequestHandler<DeleteRoleCommand, Response<string>>,
                                    IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Construcors
        public RoleCommandHandler(IMapper mapper,
                                    IStringLocalizer<SharedResources> localizer,
                                    IAuthorizationService authorizationService) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _authorizationService = authorizationService;
        }


        #endregion

        #region Handle Methods
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Success")
                return Success(result);
            //return BadRequest<string>(_localizer[SharedResourcesKeys.CreationFaild]);
            return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request.RoleName, request.Id);
            if (result == "Success")
                return Success(result);
            return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "Success")
                return Success(result);
            return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRolesAsync(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_localizer[SharedResourcesKeys.UserNotFound]);
                case "FailedToRemoveOldRoles": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
                case "FailedToAddNewRoles": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToAddNewRoles]);
                case "FailedToUpdateUserRoles": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
            }
            return Success<string>(_localizer[SharedResourcesKeys.Success]);
        }


        #endregion
    }
}
