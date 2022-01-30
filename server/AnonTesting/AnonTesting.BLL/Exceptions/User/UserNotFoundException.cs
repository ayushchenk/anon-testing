namespace AnonTesting.BLL.Exceptions.User
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(): base("User not found or password is invalid")
        {
        }
    }
}
