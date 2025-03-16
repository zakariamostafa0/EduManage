using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.UserIdentity.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.UserIdentity.Commands.Validators
{
    public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleCommand>
    {
        #region Fields
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        #endregion
        public UpdateUserRoleValidator(IStringLocalizer<SharedResources> localizer, IUserService userService, RoleManager<ApplicationRole> roleManager)
        {
            _localizer = localizer;
            _roleManager = roleManager;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
        }
        #region Constructors

        #endregion
        #region Handles Methods
        public void ApplyValidataionsRules()
        {
            RuleFor(r => r.RolesName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);
        }

        public void ApplyCustomValidataionsRules()
        {

            RuleFor(r => r.RolesName)
           .MustAsync(async (roleNames, cancellationToken) =>
           {
               if (roleNames == null || roleNames.Length == 0)
                   return false;

               var existingRoles = await _roleManager.Roles
                   .Where(role => roleNames.Contains(role.Name))
                   .Select(role => role.Name)
                   .ToListAsync(cancellationToken); // Fetch all roles in one query

               return roleNames.All(roleName => existingRoles.Contains(roleName));
           })
           .WithMessage(_localizer[SharedResourcesKeys.RoleNotExist]);
        }
        #endregion
    }
}
