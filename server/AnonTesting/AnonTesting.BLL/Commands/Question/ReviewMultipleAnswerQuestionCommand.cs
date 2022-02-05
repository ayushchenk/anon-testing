using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Commands.Question
{
    public class ReviewMultipleAnswerQuestionCommand : ReviewSpecificQuestionCommand
    {
        public ReviewMultipleAnswerQuestionCommand(CompletedQuestion question, DAL.Model.Question originalQuestion) 
            : base(question, originalQuestion)
        {
        }
    }
}
