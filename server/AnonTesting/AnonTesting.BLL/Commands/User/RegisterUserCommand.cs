using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

namespace AnonTesting.BLL.Commands.User
{
    public class RegisterUserCommand : AuthBase, ICommand<JwtSecurityToken>
    {
        public RegisterUserCommand(AuthBase auth) : base(auth)
        {
        }

        [JsonConstructor]
        public RegisterUserCommand(string email, string password) : base(email, password)
        {
        }
    }
}
