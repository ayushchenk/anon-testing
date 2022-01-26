using AnonTesting.BLL.Commands.User;
using AnonTesting.BLL.Interfaces.Commands;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AnonTesting.BLL.CommandHandlers.User
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, IdentityResult>
    {
        private readonly UserManager<DAL.Model.User> _manager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<DAL.Model.User> manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public Task<IdentityResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<CreateUserCommand, DAL.Model.User>(command);

            return _manager.CreateAsync(user, command.Password);
        }
    }
}
