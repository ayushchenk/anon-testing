using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;

namespace AnonTesting.API.Infrastructure
{
    public class JwtSettingsProvider : IJwtSettingsProvider
    {
        private const string SectionName = "Jwt";
        private readonly JwtSettings _settings;

        public JwtSettingsProvider(IConfiguration configuration)
        {
            _settings = configuration.GetSection(SectionName).Get<JwtSettings>();
        }

        public JwtSettings Provide()
        {
            return _settings;
        }
    }
}
