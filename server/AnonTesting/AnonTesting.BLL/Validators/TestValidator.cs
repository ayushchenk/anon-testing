using AnonTesting.BLL.Model;
using FluentValidation;

namespace AnonTesting.BLL.Validators
{
    public class TestValidator : AbstractValidator<TestDto>
    {
        public TestValidator()
        {
            RuleFor(test => test.Id).Empty().WithMessage("Id is set automatically");
            RuleFor(test => test.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(test => test.UserId).NotEmpty().WithMessage("UserId is required");
        }
    }
}
