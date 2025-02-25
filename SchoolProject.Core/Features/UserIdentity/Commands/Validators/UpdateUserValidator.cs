using FluentValidation;
using SchoolProject.Core.Features.UserIdentity.Commands.Models;

namespace SchoolProject.Core.Features.UserIdentity.Commands.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion
        public UpdateUserValidator(IStringLocalizer<SharedResources> localizer, IUserService userService)
        {
            _userService = userService;
            _localizer = localizer;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
        }
        #region Constructors

        #endregion
        #region Handles Methods
        public void ApplyValidataionsRules()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "50");

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "50");

            //RuleFor(r => r.PhoneNumber)
            //    .Matches(@"^(10|11|12|15)\d{8}$")
            //    .WithMessage("Phone number must be 10 digits and start with 10, 11, 12, or 15.");

            RuleFor(r => r.Address)
                //.Must(address => string.IsNullOrWhiteSpace(address) || address.Length <= 100)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(100).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "100");

            RuleFor(r => r.Country)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "50");

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .EmailAddress().WithMessage(_localizer[SharedResourcesKeys.InvalidEmail])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} 50");

            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .Matches(@"^[a-zA-Z0-9_]*$").WithMessage(_localizer[SharedResourcesKeys.InvalidUserName]) // Only letters, numbers, underscores
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} 50")
                .MinimumLength(3).WithMessage($"{_localizer[SharedResourcesKeys.Minimum]} 3");

        }

        public void ApplyCustomValidataionsRules()
        {

            RuleFor(r => r.Email)
                .MustAsync(async (model, Key, CancellationToken) =>
                    !await _userService.IsEmailExist(Key, model.Id))
                            .WithMessage(_localizer[SharedResourcesKeys.EmailExist]);

            RuleFor(r => r.UserName)
                .MustAsync(async (model, Key, CancellationToken) =>
                    !await _userService.IsUserNameExist(Key, model.Id))
                            .WithMessage(_localizer[SharedResourcesKeys.UsernameTaken]);

        }
        #endregion
    }
}
