using FluentValidation;
using SchoolProject.Core.Features.Authentication.Commands.Models;

namespace SchoolProject.Core.Features.Authentication.Commands.Validators
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public LoginValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
        }
        #endregion

        #region Handle Methods

        public void ApplyValidataionsRules()
        {
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "50");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Phone number is required.")
                //.Matches(@"^(10|11|12|15)\d{8}$")
                .WithMessage("Phone number must be 10 digits and start with 10, 11, 12, or 15.");

        }

        public void ApplyCustomValidataionsRules()
        {

        }

        #endregion
    }
}
