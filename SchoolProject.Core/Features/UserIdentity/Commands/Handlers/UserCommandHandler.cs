using SchoolProject.Core.Features.UserIdentity.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.UserIdentity.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
                                      IRequestHandler<AddUserCommand, Response<bool>>,
                                      IRequestHandler<UpdateUserCommand, Response<bool>>
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Construcors
        public UserCommandHandler(IStudentService studentService,
                                    IMapper mapper,
                                    IStringLocalizer<SharedResources> localizer,
                                    IUserService userService) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _userService = userService;
        }

        #endregion

        #region Handle Methods
        public async Task<Response<bool>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if email exists?
            var email = await _userService.UserManager.FindByEmailAsync(request.Email);
            if (email != null)
                return BadRequest<bool>(_localizer[SharedResourcesKeys.EmailExist]);
            //if user name exist?
            var username = await _userService.UserManager.FindByNameAsync(request.UserName);
            if (username != null)
                return BadRequest<bool>(_localizer[SharedResourcesKeys.UsernameTaken]);
            //created?
            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userService.UserManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest<bool>(_localizer[SharedResourcesKeys.CreationFaild], errors);
            }
            var create = Created<bool>(true);
            create.Meta = new { Id = user.Id, Name = user.FirstName + " " + user.LastName, PhoneNumber = user.PhoneNumber };
            return create;
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
        #endregion
    }
}
