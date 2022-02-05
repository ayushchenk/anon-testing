using AnonTesting.BLL.Commands.Question;
using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.DAL.Model;
using MediatR;

namespace AnonTesting.BLL.CommandHandlers.Question
{
    public class ReviewQuestionCommandHandler : ICommandHandler<ReviewQuestionCommand, bool>
    {
        private readonly ApplicationContext _context;
        private readonly IMediator _mediator;

        public ReviewQuestionCommandHandler(ApplicationContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(ReviewQuestionCommand command, CancellationToken cancellationToken)
        {
            var originalQuestion = await _context.Questions.FindAsync(command.CompletedQuestion.QuestionId);

            if(originalQuestion == null)
            {
                return false;
            }

            switch (originalQuestion.QuestionType)
            {
                case QuestionType.SingleAnswer:
                    return await _mediator.Send(new ReviewSingleAnswerQuestionCommand(command.CompletedQuestion, originalQuestion));
                case QuestionType.MultipleAnswer:
                    return await _mediator.Send(new ReviewMultipleAnswerQuestionCommand(command.CompletedQuestion, originalQuestion));
                case QuestionType.StringAnswer:
                    return await _mediator.Send(new ReviewStringAnswerQuestionCommand(command.CompletedQuestion, originalQuestion));
                default: return false;
            }
        }
    }
}
