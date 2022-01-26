using AnonTesting.BLL.Commands.User;
using AnonTesting.BLL.Exceptions.User;
using AnonTesting.BLL.Interfaces.Commands;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace AnonTesting.BLL.CommandHandlers.User
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, JwtSecurityToken>
    {
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<JwtSecurityToken> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var createResult = await _mediator.Send(new CreateUserCommand(command));

            if (!createResult.Succeeded)
            {
                throw new UnableToCreateUserException(createResult.Errors);
            }

            return await _mediator.Send(new LoginUserCommand(command));
        }
    }
}
