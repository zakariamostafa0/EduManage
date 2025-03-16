using FluentValidation;
using SchoolProject.Core.Features.Authorization.Commands.Models;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public UpdateRoleValidator(IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
        }
        #endregion

        #region Handle Methods

        public void ApplyValidataionsRules()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);

            RuleFor(r => r.RoleName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "50");
        }

        public void ApplyCustomValidataionsRules()
        {
            RuleFor(r => r.RoleName)
               .MustAsync(async (role, roleName, CancellationToken) => !await _authorizationService.IsRoleNameExistAsync(roleName, role.Id?.ToString()))
               .WithMessage(_localizer[SharedResourcesKeys.IsExists]);
        }

        #endregion
    }
}
