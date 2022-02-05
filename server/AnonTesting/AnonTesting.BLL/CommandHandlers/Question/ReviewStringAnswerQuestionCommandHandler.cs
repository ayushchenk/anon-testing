using AnonTesting.BLL.Commands.Question;
using AnonTesting.BLL.Interfaces.Commands;

namespace AnonTesting.BLL.CommandHandlers.Question
{
    public class ReviewStringAnswerQuestionCommandHandler : ICommandHandler<ReviewStringAnswerQuestionCommand, bool>
    {
        public Task<bool> Handle(ReviewStringAnswerQuestionCommand command, CancellationToken cancellationToken)
        {
            var answerString = command.OriginalQuestion.Answers.FirstOrDefault()?.Content?.Trim()?.ToLower();

            var result = !string.IsNullOrEmpty(answerString)
                && !string.IsNullOrEmpty(command.CompletedQuestion.AnswerString)
                && command.CompletedQuestion.AnswerString.Trim().ToLower() == answerString;

            return Task.FromResult(result);
        }
    }
}
