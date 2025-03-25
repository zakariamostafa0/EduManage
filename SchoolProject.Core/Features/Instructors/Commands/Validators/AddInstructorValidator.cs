using FluentValidation;
using SchoolProject.Core.Features.Instructors.Commands.Models;

namespace SchoolProject.Core.Features.Instructors.Commands.Validators
{
    public class AddInstructorValidator : AbstractValidator<AddInstructorCommand>
    {
        #region Fields
        private readonly IInstructorService _instructorService;
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public AddInstructorValidator(IStringLocalizer<SharedResources> localizer, IDepartmentService departmentService, IInstructorService instructorService)
        {
            _localizer = localizer;
            _instructorService = instructorService;
            _departmentService = departmentService;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
        }
        #endregion

        #region Handle Methods

        public void ApplyValidataionsRules()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(50).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "50");

            RuleFor(r => r.Address)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .MaximumLength(100).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "100");
            RuleFor(r => r.Position)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .MaximumLength(100).WithMessage($"{_localizer[SharedResourcesKeys.Maximum]} " + "100");
            RuleFor(r => r.Salary)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);
            RuleFor(r => r.DID)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotEmpty]);


        }

        public void ApplyCustomValidataionsRules()
        {
            RuleFor(r => r.Name)
                .MustAsync(async (Key, CancellationToken) => !await _instructorService.IsNameExist(Key))
                .WithMessage(_localizer[SharedResourcesKeys.IsExists]);

            When(d => d.DID != null, () =>
            {
                RuleFor(r => r.DID)
               //.MustAsync(async (Key, CancellationToken) => !Key.HasValue || await _departmentService.IsDepartmentExist(Key.Value))
               .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentExist(Key))
               .WithMessage(_localizer[SharedResourcesKeys.NotExists]);
            });


        }

        #endregion
    }
}
