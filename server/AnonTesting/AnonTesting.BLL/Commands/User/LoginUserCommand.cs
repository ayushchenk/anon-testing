using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using System.Text.Json.Serialization;

namespace AnonTesting.BLL.Commands.User
{
    public class LoginUserCommand : AuthBase, ICommand<Result<Token>>
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
