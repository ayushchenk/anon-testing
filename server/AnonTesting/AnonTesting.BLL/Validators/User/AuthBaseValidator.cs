using AnonTesting.BLL.Model;
using FluentValidation;

namespace AnonTesting.BLL.Validators.User
{
    public class AuthBaseValidator<TAuthBase> : AbstractValidator<TAuthBase> where TAuthBase : AuthBase
    {
        public AuthBaseValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Should be valid email");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
