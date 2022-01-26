namespace AnonTesting.BLL.Model
{
    public class AuthBase
    {
        public string Email { get; }
        public string Password { get; }

        public AuthBase(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public AuthBase(AuthBase auth)
        {
            Email = auth.Email;
            Password = auth.Password;
        }
    }
}
