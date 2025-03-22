using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.UserIdentity.Queries.Models;
using SchoolProject.Core.Features.UserIdentity.Queries.Results;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.UserIdentity.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUsersPaginationQuery, PaginatedResult<GetUsersResponse>>,
        IRequestHandler<GetUserByUsernameOrEmailQuery, Response<GetUserByUsernameOrEmailResponse>>
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IApplicationUserService _applicationUserService;
        #endregion

        #region Construcors
        public UserQueryHandler(IStudentService studentService,
                                    IMapper mapper,
                                    IStringLocalizer<SharedResources> localizer,
                                    UserManager<ApplicationUser> userManager,
                                    IApplicationUserService applicationUserService) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _userManager = userManager;
            _applicationUserService = applicationUserService;
        }

        #endregion

        #region Handle Methods
        public async Task<PaginatedResult<GetUsersResponse>> Handle(GetUsersPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginationList = await _mapper.ProjectTo<GetUsersResponse>(users)
                                        .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginationList;
        }

        public async Task<Response<GetUserByUsernameOrEmailResponse>> Handle(GetUserByUsernameOrEmailQuery request, CancellationToken cancellationToken)
        {
            var normalizedEmail = request.Email?.ToUpper();
            var normalizedUsername = request.UserName?.ToUpper();

            var user = await _userManager.Users
                         .FirstOrDefaultAsync(u => u.NormalizedEmail == request.Email || u.NormalizedUserName == request.UserName);
            if (user == null)
                return BadRequest<GetUserByUsernameOrEmailResponse>(_localizer[SharedResourcesKeys.NoEmailorUsername]);
            var userDTO = _mapper.Map<GetUserByUsernameOrEmailResponse>(user);
            return Success(userDTO);

        }

        #endregion
    }
}
