using AnonTesting.BLL.Commands.Jwt;
using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AnonTesting.BLL.CommandHandlers.Jwt
{
    public class GenerateTokenCommandHandler : ICommandHandler<GenerateTokenCommand, Token>
    {
        private readonly IJwtSettingsProvider _provider;
        private readonly SecurityTokenHandler _handler;

        public GenerateTokenCommandHandler(IJwtSettingsProvider jwtSettingsProvider, SecurityTokenHandler tokenHandler)
        {
            _provider = jwtSettingsProvider;
            _handler = tokenHandler;
        }

        public Task<Token> Handle(GenerateTokenCommand command, CancellationToken cancellationToken)
        {
            var settings = _provider.Provide();

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, command.User.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in command.UserRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var securityToken = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                expires: DateTime.UtcNow.AddHours(settings.ExpiresMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(settings.SigningKey, SecurityAlgorithms.HmacSha256)
            );

            string tokenValue = _handler.WriteToken(securityToken);

            var token = new Token()
            {
                Value = tokenValue,
                UserId = command.User.Id,
                ExpriesOn = securityToken.ValidTo
            };

            return Task.FromResult(token);
        }
    }
}
