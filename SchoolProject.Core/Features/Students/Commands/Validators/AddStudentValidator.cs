using FluentValidation;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _localizer = localizer;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
            _departmentService = departmentService;
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

            RuleFor(r => r.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                //.Matches(@"^(10|11|12|15)\d{8}$")
                .WithMessage("Phone number must be 10 digits and start with 10, 11, 12, or 15.");

        }

        public void ApplyCustomValidataionsRules()
        {
            RuleFor(r => r.Name)
                .MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
                .WithMessage(_localizer[SharedResourcesKeys.IsExists]);

            When(d => d.DepartmentId != null, () =>
            {
                RuleFor(r => r.DepartmentId)
               //.MustAsync(async (Key, CancellationToken) => !Key.HasValue || await _departmentService.IsDepartmentExist(Key.Value))
               .MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentExist(Key.Value))
               .WithMessage(_localizer[SharedResourcesKeys.NotExists]);
            });


        }

        #endregion
    }
}
