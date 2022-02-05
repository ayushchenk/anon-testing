using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Commands.Question
{
    public class ReviewSingleAnswerQuestionCommand : ReviewSpecificQuestionCommand
    {
        public ReviewSingleAnswerQuestionCommand(CompletedQuestion question, DAL.Model.Question originalQuestion)
            : base(question, originalQuestion)
        {
        }
    }
}
