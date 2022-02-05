using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Commands.Question
{
    public class ReviewStringAnswerQuestionCommand : ReviewSpecificQuestionCommand
    {
        public ReviewStringAnswerQuestionCommand(CompletedQuestion question, DAL.Model.Question originalQuestion)
            : base(question, originalQuestion)
        {
        }
    }
}
