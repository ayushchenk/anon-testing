using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AnonTesting.BLL.Model
{
    public class JwtSettings
    {
        public string Issuer { set; get; } = String.Empty;
        public string Audience { set; get; } = String.Empty;
        public string Key { set; get; } = String.Empty;
        public int ExpiresMinutes { get; set; } = 60;

        public SymmetricSecurityKey SigningKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
