using AnonTesting.BLL.Commands.Jwt;
using AnonTesting.BLL.Commands.User;
using AnonTesting.BLL.Exceptions.User;
using AnonTesting.BLL.Interfaces.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace AnonTesting.BLL.CommandHandlers.User
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, JwtSecurityToken>
    {
        private readonly UserManager<DAL.Model.User> _manager;
        private readonly IMediator _mediator;

        public LoginUserCommandHandler(UserManager<DAL.Model.User> manager, IMediator mediator)
        {
            _manager = manager;
            _mediator = mediator;
        }

        public async Task<JwtSecurityToken> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _manager.FindByEmailAsync(command.Email);

            if (user != null && await _manager.CheckPasswordAsync(user, command.Password))
            {
                var userRoles = await _manager.GetRolesAsync(user);

                var tokenCommand = new GenerateTokenCommand(user.Email, userRoles);

                return await _mediator.Send(tokenCommand);
            }

            throw new UserNotFoundException();
        }
    }
}
