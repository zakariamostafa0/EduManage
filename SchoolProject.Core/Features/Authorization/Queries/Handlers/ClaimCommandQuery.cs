using SchoolProject.Core.Features.Authorization.Queries.Models;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimCommandQuery : ResponseHandler,
                                    IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResult>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Construcors
        public ClaimCommandQuery(IMapper mapper,
                                    IStringLocalizer<SharedResources> localizer,
                                    IAuthorizationService authorizationService) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Handle Methods
        public async Task<Response<ManageUserClaimsResult>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.GetUserClaimsAsync(request.UserId);
            if (!result.Success)
                return NotFound<ManageUserClaimsResult>(_localizer[SharedResourcesKeys.UserNotFound]);
            return Success(result);

        }
        #endregion
    }
}
