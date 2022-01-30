using AnonTesting.BLL.Commands.Jwt;
using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Interfaces.Commands;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AnonTesting.BLL.CommandHandlers.Jwt
{
    public class GenerateTokenCommandHandler : ICommandHandler<GenerateTokenCommand, JwtSecurityToken>
    {
        private readonly IJwtSettingsProvider _provider;

        public GenerateTokenCommandHandler(IJwtSettingsProvider jwtSettingsProvider)
        {
            _provider = jwtSettingsProvider;
        }

        public Task<JwtSecurityToken> Handle(GenerateTokenCommand command, CancellationToken cancellationToken)
        {
            var settings = _provider.Provide();

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, command.UserEmail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in command.UserRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return Task.FromResult(new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                expires: DateTime.Now.AddHours(settings.ExpiresMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(settings.SigningKey, SecurityAlgorithms.HmacSha256)
            ));
        }
    }
}
