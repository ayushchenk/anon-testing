using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Interfaces
{
    public interface IJwtSettingsProvider
    {
        JwtSettings Provide();
    }
}
