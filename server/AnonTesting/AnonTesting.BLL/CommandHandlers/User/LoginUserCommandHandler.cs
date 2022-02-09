using AnonTesting.BLL.Commands.Jwt;
using AnonTesting.BLL.Commands.User;
using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AnonTesting.BLL.CommandHandlers.User
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, Result<Token>>
    {
        private readonly UserManager<DAL.Model.User> _manager;
        private readonly IMediator _mediator;

        public LoginUserCommandHandler(UserManager<DAL.Model.User> manager, IMediator mediator)
        {
            _manager = manager;
            _mediator = mediator;
        }

        public async Task<Result<Token>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _manager.FindByEmailAsync(command.Email);

            if (user != null && await _manager.CheckPasswordAsync(user, command.Password))
            {
                var userRoles = await _manager.GetRolesAsync(user);

                var tokenCommand = new GenerateTokenCommand(user, userRoles);

                var token = await _mediator.Send(tokenCommand);

                return Result.Success(token);
            }

            return Result.Failure<Token>("User does not exist");
        }
    }
}
