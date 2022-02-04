using AnonTesting.BLL.Model;
using AnonTesting.BLL.Validators.Answer;
using FluentValidation;

namespace AnonTesting.BLL.Validators.Question
{
    public class QuestionDtoValidator : AbstractValidator<QuestionDto>
    {
        public QuestionDtoValidator()
        {
            RuleFor(question => question.Id).Empty().WithMessage("Id is set automatically");
            RuleFor(question => question.TestId).Empty().WithMessage("TestId is set automatically");
            RuleFor(question => question.Content).NotEmpty().WithMessage("Content is required");
            RuleFor(question => question.Answers).NotEmpty().WithMessage("Answers are required");
            RuleForEach(question => question.Answers).SetValidator((answer) => new AnswerDtoValidator());
            ValidateAnswers();
        }

        private void ValidateAnswers()
        {
            ValidateWhenSingleAnswerType();
            ValidateWhenMultipleAnswersType();
            ValidateWhenStringAnswerType();
        }

        private void ValidateWhenSingleAnswerType()
        {
            RuleFor(q => q.Answers).Custom((answers, context) =>
            {
                int correctAnswers = answers.Count(a => a.IsCorrect);

                if (correctAnswers != 1)
                {
                    context.AddFailure("Answers", "Single answer questions should only have one correct answer");
                }

            }).When(q => q.QuestionType == DAL.Model.QuestionType.SingleAnswer);
        }

        private void ValidateWhenMultipleAnswersType()
        {
            RuleFor(q => q.Answers).Custom((answers, context) =>
            {
                int correctAnswers = answers.Count(a => a.IsCorrect);

                if (correctAnswers == 0)
                {
                    context.AddFailure("Answers", "Multiple answers questions should have at least one correct answer");
                }

            }).When(q => q.QuestionType == DAL.Model.QuestionType.MultipleAnswers);
        }

        private void ValidateWhenStringAnswerType()
        {
            RuleFor(q => q.Answers).Custom((answers, context) =>
            {
                if(answers.Count() != 1 || answers.Count(a => a.IsCorrect) != 1)
                {
                    context.AddFailure("Answers", "String answer questions should only have one answer");
                }

            }).When(q => q.QuestionType == DAL.Model.QuestionType.StringAnswer);
        }
    }
}
