using FluentValidation;
using SchoolProject.Core.Features.UserIdentity.Commands.Models;

namespace SchoolProject.Core.Features.UserIdentity.Commands.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion
        public AddUserValidator(IStringLocalizer<SharedResources> localizer)
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
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "50");

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "50");

            RuleFor(r => r.PhoneNumber)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .Matches(@"^(10|11|12|15)\d{8}$")
                .WithMessage("Phone number must be 10 digits and start with 10, 11, 12, or 15.");

            RuleFor(r => r.Address)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(100).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "100");

            //RuleFor(r => r.Country)
            //    .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            //    .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "50");

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

            RuleFor(r => r.Password)
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
