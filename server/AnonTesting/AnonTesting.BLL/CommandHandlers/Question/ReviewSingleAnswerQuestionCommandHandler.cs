using AnonTesting.BLL.Commands.Question;
using AnonTesting.BLL.Interfaces.Commands;

namespace AnonTesting.BLL.CommandHandlers.Question
{
    public class ReviewSingleAnswerQuestionCommandHandler : ICommandHandler<ReviewSingleAnswerQuestionCommand, bool>
    {
        public Task<bool> Handle(ReviewSingleAnswerQuestionCommand command, CancellationToken cancellationToken)
        {
            var correctAnswer = command.OriginalQuestion.Answers.FirstOrDefault(a => a.IsCorrect);

            bool result = correctAnswer != null
                && command.CompletedQuestion.Answers != null
                && command.CompletedQuestion.Answers.Count() == 1
                && command.CompletedQuestion.Answers!.First() == correctAnswer.Id;

            return Task.FromResult(result);
        }
    }
}
