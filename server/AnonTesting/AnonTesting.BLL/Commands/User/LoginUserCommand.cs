using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

namespace AnonTesting.BLL.Commands.User
{
    public class LoginUserCommand : AuthBase, ICommand<JwtSecurityToken>
    {
        public LoginUserCommand(AuthBase auth) : base(auth)
        {
        }

        [JsonConstructor]
        public LoginUserCommand(string email, string password) : base(email, password)
        {
        }
    }
}
