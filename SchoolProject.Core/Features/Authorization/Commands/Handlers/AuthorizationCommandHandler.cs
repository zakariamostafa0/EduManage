using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class AuthorizationCommandHandler : ResponseHandler,
                                    IRequestHandler<AddRoleCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Construcors
        public AuthorizationCommandHandler(IMapper mapper,
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
            return BadRequest<string>(_localizer[SharedResourcesKeys.CreationFaild]);
        }
        #endregion
    }
}
