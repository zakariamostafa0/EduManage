using FluentValidation;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion

        #region Constructors
        public AddStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidataionsRules();
            ApplyCustomValidataionsRules();
        }
        #endregion

        #region Handle Methods
        public void ApplyValidataionsRules()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithErrorCode("1").WithMessage("must not Empty!")
                .NotNull().WithMessage("must not empty!")
                .MaximumLength(50).WithMessage("maximum lenght should not be greater than 50");

            RuleFor(r => r.Address)
                .NotEmpty().WithErrorCode("1").WithMessage("must not Empty!")
                .NotNull().WithMessage("must not empty!")
                .MaximumLength(100).WithMessage("maximum lenght should not be greater than 100");
        }

        public void ApplyCustomValidataionsRules()
        {
            RuleFor(r => r.Name)
                .MustAsync(async (Key, CancellationToken) =>
                    !await _studentService.IsNameExist(Key))
                            .WithMessage("Is Exists");
        }

        #endregion
    }
}
