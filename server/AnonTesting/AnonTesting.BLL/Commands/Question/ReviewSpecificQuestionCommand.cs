using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Commands.Question
{
    public class ReviewSpecificQuestionCommand : ReviewQuestionCommand
    {
        public DAL.Model.Question OriginalQuestion { get; }

        public ReviewSpecificQuestionCommand(CompletedQuestion question, DAL.Model.Question originalQuestion)
            : base(question)
        {
            OriginalQuestion = originalQuestion;
        }
    }
}
