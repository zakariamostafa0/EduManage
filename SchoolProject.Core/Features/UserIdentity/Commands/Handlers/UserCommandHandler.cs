using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.UserIdentity.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.UserIdentity.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
                                      IRequestHandler<AddUserCommand, Response<bool>>,
                                      IRequestHandler<UpdateUserCommand, Response<bool>>,
                                      IRequestHandler<ChangePasswordCommand, Response<string>>,
                                      IRequestHandler<SendEmailConfirmationAgainQuery, Response<string>>,
                                      IRequestHandler<SendEmailResetPasswordCommand, Response<string>>,
                                      IRequestHandler<ResetPasswordCommand, Response<string>>
    //IRequestHandler<UpdateUserRoleCommand, Response<bool>>
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IApplicationUserService _applicationUserService;
        #endregion

        #region Construcors
        public UserCommandHandler(IStudentService studentService,
                                    IMapper mapper,
                                    IStringLocalizer<SharedResources> localizer,
                                    IUserService userService,
                                    RoleManager<ApplicationRole> roleManager,
                                    IApplicationUserService applicationUserService) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _userService = userService;
            _roleManager = roleManager;
            _applicationUserService = applicationUserService;
        }

        #endregion

        #region Handle Methods
        public async Task<Response<bool>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUser>(request);

            var result = await _applicationUserService.AddUserAsync(user, request.Password);
            switch (result)
            {
                case "EmailExist": return NotFound<bool>(_localizer[SharedResourcesKeys.EmailExist]);
                case "UserNameExist": return BadRequest<bool>(_localizer[SharedResourcesKeys.UsernameTaken]);
                case "FailedToCreateUser": return BadRequest<bool>(_localizer[SharedResourcesKeys.CreationFaild]);
                case "Success":
                    {
                        var create = Created(true);
                        create.Meta = new { Id = user.Id, Name = user.FirstName + " " + user.LastName, PhoneNumber = user.PhoneNumber };
                        return create;
                    }

                default: return BadRequest<bool>(result);
            }
        }

        public async Task<Response<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //check if the user exists
            var user = await _userService.UserManager.FindByIdAsync(request.Id);
            if (user == null)
                return NotFound<bool>(_localizer[SharedResourcesKeys.NotFound]);
            //if found ==> mapping
            _mapper.Map(request, user);
            //update
            var result = await _userService.UserManager.UpdateAsync(user);
            //return the result
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest<bool>(_localizer[SharedResourcesKeys.UpdatedFaild], errors);
            }

            var response = Success<bool>(true);
            response.Meta = new { Id = user.Id, Name = user.FirstName + " " + user.LastName, Email = user.Email, UserName = user.UserName };
            return response;
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.UserManager.FindByIdAsync(request.Id);
            if (user == null)
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound]);

            //var hasPassword = await _userService.UserManager.HasPasswordAsync(user);
            //if (!hasPassword)
            //{
            //    return NotFound<string>(_localizer[SharedResourcesKeys.DoesnotHasPassword]);
            //}
            var changePassowrd = await _userService.UserManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!changePassowrd.Succeeded)
            {
                var errors = changePassowrd.Errors.Select(e => e.Description).ToList();
                if (errors.Contains("Incorrect password."))
                    return BadRequest<string>(_localizer[SharedResourcesKeys.IncorrectPassword], errors);
                return BadRequest<string>(_localizer[SharedResourcesKeys.UpdatedFaild], errors);
            }
            return Success<string>(_localizer[SharedResourcesKeys.PasswordChanged]);
        }
        public async Task<Response<string>> Handle(SendEmailConfirmationAgainQuery request, CancellationToken cancellationToken)
        {
            var result = await _applicationUserService.SendEmailConfirmationAgain(request.Email);
            if (result != "Success")
                return BadRequest<string>(_localizer[SharedResourcesKeys.ErrorEmailConfirmation]);
            return Success(result);
        }

        public async Task<Response<string>> Handle(SendEmailResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _applicationUserService.SendResetPasswordEmail(request.Email);
            if (result != "Success")
                return BadRequest<string>(_localizer[SharedResourcesKeys.UserNotFound]);
            return Success<string>("");
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _applicationUserService.ResetPassword(request.UserId, request.Token, request.NewPassword);
            if (result != "Success")
                return BadRequest<string>(result);
            return Success<string>("");
        }

        //public async Task<Response<bool>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        //{
        //    var user = await _userService.UserManager.FindByIdAsync(request.Id);
        //    if (user == null)
        //        return NotFound<bool>(_localizer[SharedResourcesKeys.NotFound]);

        //    bool isAddingRoles = _httpContextAccessor.HttpContext?.Request.Method == HttpMethods.Post;

        //    IdentityResult result = isAddingRoles
        //        ? await _userService.UserManager.AddToRolesAsync(user, request.RolesName)
        //        : await _userService.UserManager.RemoveFromRolesAsync(user, request.RolesName);

        //    if (!result.Succeeded)
        //    {
        //        var errors = result.Errors.Select(e => e.Description).ToList();
        //        return BadRequest<bool>(isAddingRoles
        //            ? _localizer[SharedResourcesKeys.UpdatedFaild]
        //            : _localizer[SharedResourcesKeys.RemovalFailed], errors);
        //    }

        //    return Success<bool>(true);
        //}

        #endregion
    }
}
