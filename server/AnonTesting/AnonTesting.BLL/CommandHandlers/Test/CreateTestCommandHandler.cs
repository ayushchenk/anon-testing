using AnonTesting.BLL.Commands.Test;
using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using AnonTesting.DAL.Model;
using AutoMapper;

namespace AnonTesting.BLL.CommandHandlers.Test
{
    public class CreateTestCommandHandler : ICommandHandler<CreateTestCommand, Guid>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CreateTestCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTestCommand command, CancellationToken cancellationToken)
        {
            var testEntity = _mapper.Map<TestDto, DAL.Model.Test>(command.Test);

            _context.Tests.Add(testEntity);
            await _context.SaveChangesAsync();

            return testEntity.Id;
        }
    }
}
