

using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.UserIdentity.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.UserIdentity.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
                                      IRequestHandler<AddUserCommand, Response<bool>>
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Construcors
        public UserCommandHandler(IStudentService studentService,
                                    IMapper mapper,
                                    IStringLocalizer<SharedResources> localizer,
                                    UserManager<ApplicationUser> userManager) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _userManager = userManager;
        }

        #endregion

        #region Handle Methods
        public async Task<Response<bool>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if email exists?
            var email = await _userManager.FindByEmailAsync(request.Email);
            if (email != null)
                return BadRequest<bool>(_localizer[SharedResourcesKeys.EmailExist]);
            //if user name exist?
            var username = await _userManager.FindByNameAsync(request.UserName);
            if (username != null)
                return BadRequest<bool>(_localizer[SharedResourcesKeys.UsernameTaken]);
            //created?
            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest<bool>(_localizer[SharedResourcesKeys.CreationFaild], errors);
            }
            var create = Created<bool>(true);
            create.Meta = new { Id = user.Id, Name = user.FirstName + " " + user.LastName, PhoneNumber = user.PhoneNumber };
            return create;
        }
        #endregion
    }
}
