using AnonTesting.BLL.Model;
using AnonTesting.BLL.Validators.Question;
using FluentValidation;

namespace AnonTesting.BLL.Validators.Test
{
    public class TestDtoValidator : AbstractValidator<TestDto>
    {
        public TestDtoValidator()
        {
            RuleFor(test => test.Id).Empty().WithMessage("Id is set automatically");
            RuleFor(test => test.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(test => test.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(test => test.Questions).NotEmpty().WithMessage("Questions are required");
            RuleForEach(test => test.Questions).SetValidator((test) => new QuestionDtoValidator());
        }
    }
}
