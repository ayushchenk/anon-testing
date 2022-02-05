using AnonTesting.BLL.Commands.Question;
using AnonTesting.BLL.Commands.Test;
using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using AutoMapper;
using MediatR;

namespace AnonTesting.BLL.CommandHandlers.Test
{
    public class ReviewTestCommandHandler : ICommandHandler<ReviewTestCommand, TestResultDto>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ReviewTestCommandHandler(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<TestResultDto> Handle(ReviewTestCommand command, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<ReviewTestCommand, TestResultDto>(command);

            foreach (var completedQuestion in command.Questions)
            {
                var isCorrect = await _mediator.Send(new ReviewQuestionCommand(completedQuestion));

                if (isCorrect)
                {
                    result.CorrectQuestions++;
                }
            }

            return result;
        }
    }
}
