using AnonTesting.BLL.Commands.Test;
using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using AnonTesting.DAL.Interfaces;
using AutoMapper;

namespace AnonTesting.BLL.CommandHandlers.Test
{
    public class CreateTestCommandHandler : ICommandHandler<CreateTestCommand, Guid>
    {
        private readonly IEntityRepository<DAL.Model.Test> _repository;
        private readonly IMapper _mapper;

        public CreateTestCommandHandler(IEntityRepository<DAL.Model.Test> testRepository, IMapper mapper)
        {
            _repository = testRepository;
            _mapper = mapper;
        }

        public Task<Guid> Handle(CreateTestCommand command, CancellationToken cancellationToken)
        {
            var testEntity = _mapper.Map<TestDto, DAL.Model.Test>(command.Test);

            return _repository.CreateAsync(testEntity).AsTask();
        }
    }
}
