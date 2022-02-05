using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Commands.Test
{
    public class ReviewTestCommand : ICommand<TestResultDto>
    {
        public Guid TestId { get; }
        public string ContestantName { get; }
        public IEnumerable<CompletedQuestion> Questions { get; }

        public ReviewTestCommand(Guid testId, string contestantName, IEnumerable<CompletedQuestion> questions)
        {
            TestId = testId;
            ContestantName = contestantName;
            Questions = questions;
        }

        public ReviewTestCommand(CompleteTestCommand completeCommand)
        {
            TestId = completeCommand.TestId;
            ContestantName = completeCommand.ContestantName;
            Questions = completeCommand.Questions;
        }
    }
}
