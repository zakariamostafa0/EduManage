using FluentValidation;
using SchoolProject.Core.Features.UserIdentity.Commands.Models;

namespace SchoolProject.Core.Features.UserIdentity.Commands.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion
        public ChangePasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
        }
        #region Constructors

        #endregion
        #region Handles Methods
        public void ApplyValidataionsRules()
        {

            RuleFor(r => r.CurrentPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);
            //.MinimumLength(8).WithMessage($"{_localizer[SharedResourcesKeys.Minimum]} 8")
            //.Matches(@"[A-Z]").WithMessage(_localizer[SharedResourcesKeys.PasswordUppercase])
            //.Matches(@"[a-z]").WithMessage(_localizer[SharedResourcesKeys.PasswordLowercase])
            //.Matches(@"[0-9]").WithMessage(_localizer[SharedResourcesKeys.PasswordDigit])
            //.Matches(@"[\@\!\#\$\%\^\&\*]").WithMessage(_localizer[SharedResourcesKeys.PasswordSpecial]);
            RuleFor(r => r.NewPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);
            //.MinimumLength(8).WithMessage($"{_localizer[SharedResourcesKeys.Minimum]} 8")
            //.Matches(@"[A-Z]").WithMessage(_localizer[SharedResourcesKeys.PasswordUppercase])
            //.Matches(@"[a-z]").WithMessage(_localizer[SharedResourcesKeys.PasswordLowercase])
            //.Matches(@"[0-9]").WithMessage(_localizer[SharedResourcesKeys.PasswordDigit])
            //.Matches(@"[\@\!\#\$\%\^\&\*]").WithMessage(_localizer[SharedResourcesKeys.PasswordSpecial]);
        }

        public void ApplyCustomValidataionsRules()
        {


        }
        #endregion
    }
}
