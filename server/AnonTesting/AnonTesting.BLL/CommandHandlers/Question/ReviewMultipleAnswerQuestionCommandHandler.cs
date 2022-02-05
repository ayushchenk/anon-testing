using AnonTesting.BLL.Commands.Question;
using AnonTesting.BLL.Interfaces.Commands;

namespace AnonTesting.BLL.CommandHandlers.Question
{
    public class ReviewMultipleAnswerQuestionCommandHandler : ICommandHandler<ReviewMultipleAnswerQuestionCommand, bool>
    {
        public Task<bool> Handle(ReviewMultipleAnswerQuestionCommand command, CancellationToken cancellationToken)
        {
            var correctAnswersIds = command.OriginalQuestion.Answers
                .Where(a => a.IsCorrect)
                .Select(a => a.Id)
                .OrderBy(a => a);

            var result = correctAnswersIds.Any()
                && command.CompletedQuestion.Answers != null
                && command.CompletedQuestion.Answers.Any()
                && Enumerable.SequenceEqual(correctAnswersIds, command.CompletedQuestion.Answers.OrderBy(a => a));

            return Task.FromResult(result);
        }
    }
}
