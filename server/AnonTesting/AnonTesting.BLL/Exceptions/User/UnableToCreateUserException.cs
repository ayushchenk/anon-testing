using Microsoft.AspNetCore.Identity;

namespace AnonTesting.BLL.Exceptions.User
{
    public class UnableToCreateUserException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; }

        public UnableToCreateUserException(IEnumerable<IdentityError> errors) : base(ConstructMessage(errors))
        {
            Errors = errors;
        }

        public UnableToCreateUserException(string message) : base(message)
        {
            Errors = Enumerable.Empty<IdentityError>();
        }

        private static string ConstructMessage(IEnumerable<IdentityError> errors)
        {
            return errors.Any() 
                ? string.Join(';', errors.Select(e => e.Description)) 
                : string.Empty;
        }
    }
}
