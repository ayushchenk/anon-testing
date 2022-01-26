using AnonTesting.BLL.Commands.User;
using AnonTesting.BLL.Exceptions.User;
using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Interfaces.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AnonTesting.BLL.CommandHandlers.User
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, JwtSecurityToken>
    {
        private readonly UserManager<DAL.Model.User> _manager;
        private readonly IJwtSettingsProvider _provider;

        public LoginUserCommandHandler(UserManager<DAL.Model.User> manager, IJwtSettingsProvider provider)
        {
            _manager = manager;
            _provider = provider;
        }

        public async Task<JwtSecurityToken> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _manager.FindByEmailAsync(command.Email);

            if (user != null && await _manager.CheckPasswordAsync(user, command.Password))
            {
                var settings = _provider.Provide();

                var userRoles = await _manager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                return new JwtSecurityToken(
                    issuer: settings.Issuer,
                    audience: settings.Audience,
                    expires: DateTime.Now.AddHours(settings.ExpiresMinutes),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(settings.SigningKey, SecurityAlgorithms.HmacSha256)
                );
            }

            throw new UserNotFoundException();
        }
    }
}
