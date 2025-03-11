using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
                                                IRequestHandler<LoginCommand, Response<JwtAuthResult>>,
                                                IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Construcors
        public AuthenticationCommandHandler(IMapper mapper,
                                    IStringLocalizer<SharedResources> localizer,
                                    UserManager<ApplicationUser> userManager,
                                    SignInManager<ApplicationUser> signInManager,
                                    IAuthenticationService authenticationService) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Handle Methods
        public async Task<Response<JwtAuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //check it user exist
            var username = request.UserName.ToUpper();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.NormalizedUserName == username || u.NormalizedEmail == username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return BadRequest<JwtAuthResult>(_localizer[SharedResourcesKeys.Invalidlogin]);
            //var loginResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //if (loginResult.Succeeded)
            //    return BadRequest<string>(_localizer[SharedResourcesKeys.Invalidlogin]);

            //generate token
            var result = await _authenticationService.GetJWTToken(user);
            //return token
            return Success<JwtAuthResult>(result);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.AlgorithmIsWrong]);
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.TokenIsNotExpired]);
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>(_localizer[SharedResourcesKeys.RefreshTokenIsExpired]);
            }
            var (UserId, ExpireDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            var result = await _authenticationService.GetRefreshToken(user, jwtToken, ExpireDate, request.RefreshToken);
            return Success(result);
        }
        #endregion

    }
}
