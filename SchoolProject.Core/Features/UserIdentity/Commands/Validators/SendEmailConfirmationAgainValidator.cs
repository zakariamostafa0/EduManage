using FluentValidation;
using SchoolProject.Core.Features.UserIdentity.Commands.Models;

namespace SchoolProject.Core.Features.UserIdentity.Commands.Validators
{
    internal class SendEmailConfirmationAgainValidator : AbstractValidator<SendEmailConfirmationAgainQuery>
    {
        #region Fields

        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public SendEmailConfirmationAgainValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
        }
        #endregion

        #region Handle Methods

        public void ApplyValidataionsRules()
        {
            RuleFor(r => r.Email)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .EmailAddress().WithMessage(_localizer[SharedResourcesKeys.InvalidEmail])
               .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} 50");

        }

        public void ApplyCustomValidataionsRules()
        {

        }

        #endregion
    }
}
