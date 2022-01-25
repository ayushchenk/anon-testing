using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Commands.Test
{
    public class CreateTestCommand : ICommand<Guid>
    {
        public TestDto Test { get; }

        public CreateTestCommand(TestDto test)
        {
            Test = test;
        }
    }
}
