using FluentValidation;
using SchoolProject.Core.Features.Emails.Commands.Models;

namespace SchoolProject.Core.Features.Emails.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public SendEmailValidator(IStringLocalizer<SharedResources> localizer)
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
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "100");

            RuleFor(r => r.Message)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(10000).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "10000");
        }

        public void ApplyCustomValidataionsRules()
        {

        }

        #endregion
    }
}
