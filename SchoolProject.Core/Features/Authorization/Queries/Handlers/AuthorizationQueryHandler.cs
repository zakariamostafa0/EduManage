using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class AuthorizationQueryHandler : ResponseHandler,
                                        IRequestHandler<GetRolesListQuery, Response<List<GetRoleResult>>>,
                                        IRequestHandler<GetRoleByIdQuery, Response<GetRoleResult>>

    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Construcors
        public AuthorizationQueryHandler(IMapper mapper,
                                    IStringLocalizer<SharedResources> localizer,
                                    IAuthorizationService authorizationService) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _authorizationService = authorizationService;
        }



        #endregion

        #region Handle Methods
        public async Task<Response<List<GetRoleResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetAllRolesAsync();
            var result = _mapper.Map<List<GetRoleResult>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRoleResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleById(request.Id);
            if (role == null)
                return NotFound<GetRoleResult>(_localizer[SharedResourcesKeys.NotExists]);
            var result = _mapper.Map<GetRoleResult>(role);
            return Success(result);
        }

        #endregion
    }
}
