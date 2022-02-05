using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Commands.Question
{
    public class ReviewQuestionCommand : ICommand<bool>
    {
        public CompletedQuestion CompletedQuestion { get; }

        public ReviewQuestionCommand(CompletedQuestion question)
        {
            CompletedQuestion = question;
        }
    }
}
