using AnonTesting.BLL.Model;
using FluentValidation;

namespace AnonTesting.BLL.Validators.Answer
{
    public class AnswerDtoValidator : AbstractValidator<AnswerDto>
    {
        public AnswerDtoValidator()
        {
            RuleFor(answer => answer.Id).Empty().WithMessage("Id is set automatically");
            RuleFor(answer => answer.QuestionId).Empty().WithMessage("QuestionId is set automatically");
            RuleFor(answer => answer.Content).NotEmpty().WithMessage("Content is required");
        }
    }
}
