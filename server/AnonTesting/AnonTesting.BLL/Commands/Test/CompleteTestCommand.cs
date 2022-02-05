using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Commands.Test
{
    public class CompleteTestCommand : ICommand<TestResultDto>
    {
        public Guid TestId { get; }
        public string ContestantName { get; }
        public IEnumerable<CompletedQuestion> Questions { get; }

        public CompleteTestCommand(Guid testId, string contestantName, IEnumerable<CompletedQuestion> questions)
        {
            TestId = testId;
            ContestantName = contestantName;
            Questions = questions;
        }
    }
}
