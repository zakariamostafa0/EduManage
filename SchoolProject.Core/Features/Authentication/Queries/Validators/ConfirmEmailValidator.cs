using FluentValidation;
using SchoolProject.Core.Features.Authentication.Queries.Models;

namespace SchoolProject.Core.Features.Authentication.Queries.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields

        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public ConfirmEmailValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
        }
        #endregion

        #region Handle Methods

        public void ApplyValidataionsRules()
        {
            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(100).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "100");

            RuleFor(r => r.Code)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);

        }

        public void ApplyCustomValidataionsRules()
        {

        }

        #endregion
    }
}
