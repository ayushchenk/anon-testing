using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Commands.Test
{
    public class ReviewTestCommand : CompleteTestCommand
    {
        public ReviewTestCommand(CompleteTestCommand command) : base(command.TestId, command.ContestantName, command.Questions)
        {
        }
    }
}
