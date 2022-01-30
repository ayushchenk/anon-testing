using AnonTesting.BLL.Interfaces.Commands;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace AnonTesting.BLL.Commands.Jwt
{
    public class GenerateTokenCommand : ICommand<JwtSecurityToken>
    {
        public string UserEmail { get; }
        public IEnumerable<string> UserRoles { get; }

        public GenerateTokenCommand(string email, IEnumerable<string> roles)
        {
            UserEmail = email;
            UserRoles = roles;
        }
    }
}
