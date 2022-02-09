using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Commands.Jwt
{
    public class GenerateTokenCommand : ICommand<Token>
    {
        public DAL.Model.User User { get; }
        public IEnumerable<string> UserRoles { get; }

        public GenerateTokenCommand(DAL.Model.User user, IEnumerable<string> roles)
        {
            User = user;
            UserRoles = roles;
        }
    }
}
