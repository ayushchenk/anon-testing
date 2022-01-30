using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using Microsoft.AspNetCore.Identity;

namespace AnonTesting.BLL.Commands.User
{
    public class CreateUserCommand : AuthBase, ICommand<IdentityResult>
    {
        public CreateUserCommand(string email, string password): base(email, password)
        {
        }

        public CreateUserCommand(RegisterUserCommand command): base(command)
        {
        }
    }
}
