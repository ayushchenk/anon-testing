using AnonTesting.BLL.Commands.Test;
using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using AnonTesting.DAL.Model;
using AutoMapper;
using MediatR;

namespace AnonTesting.BLL.CommandHandlers.Test
{
    public class CompleteTestCommandHandler : ICommandHandler<CompleteTestCommand, TestResultDto>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ApplicationContext _context;

        public CompleteTestCommandHandler(IMapper mapper, IMediator mediator, ApplicationContext context)
        {
            _mapper = mapper;
            _mediator = mediator;
            _context = context;
        }

        public async Task<TestResultDto> Handle(CompleteTestCommand command, CancellationToken cancellationToken)
        {
            var resultDto = await _mediator.Send(new ReviewTestCommand(command));

            var result = _mapper.Map<TestResultDto, TestResult>(resultDto);

            _context.TestResults.Add(result);
            await _context.SaveChangesAsync();

            resultDto.Id = result.Id;

            return resultDto;
        }
    }
}
