using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using System.Text.Json.Serialization;

namespace AnonTesting.BLL.Commands.User
{
    public class RegisterUserCommand : AuthBase, ICommand<Result<Token>>
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
