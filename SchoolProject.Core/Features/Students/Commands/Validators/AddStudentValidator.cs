using FluentValidation;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer)
        {
            _studentService = studentService;
            _localizer = localizer;
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
        }

        public void ApplyCustomValidataionsRules()
        {
            RuleFor(r => r.Name)
                .MustAsync(async (Key, CancellationToken) =>
                    !await _studentService.IsNameExist(Key))
                            .WithMessage(_localizer[SharedResourcesKeys.IsExists]);
        }

        #endregion
    }
}
