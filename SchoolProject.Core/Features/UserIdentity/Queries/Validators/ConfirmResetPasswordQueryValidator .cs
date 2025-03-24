using FluentValidation;
using SchoolProject.Core.Features.UserIdentity.Queries.Models;

namespace SchoolProject.Core.Features.UserIdentity.Queries.Validators
{
    public class ConfirmResetPasswordQueryValidator : AbstractValidator<ConfirmResetPasswordQuery>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public ConfirmResetPasswordQueryValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Code)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(r => r.Email)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .EmailAddress().WithMessage(_localizer[SharedResourcesKeys.InvalidEmail])
               .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} 50");

        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion

    }
}