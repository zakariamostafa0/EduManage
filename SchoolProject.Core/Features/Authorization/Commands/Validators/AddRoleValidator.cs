using FluentValidation;
using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public AddRoleValidator(IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService)
        {
            _localizer = localizer;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
            _authorizationService = authorizationService;
        }
        #endregion

        #region Handle Methods

        public void ApplyValidataionsRules()
        {
            RuleFor(r => r.RoleName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "50");
        }

        public void ApplyCustomValidataionsRules()
        {
            RuleFor(r => r.RoleName)
               .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleNameExistAsync(Key))
               .WithMessage(_localizer[SharedResourcesKeys.IsExists]);
        }

        #endregion
    }
}
