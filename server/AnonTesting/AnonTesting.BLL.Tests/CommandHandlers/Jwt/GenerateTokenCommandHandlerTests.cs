using AnonTesting.BLL.CommandHandlers.Jwt;
using AnonTesting.BLL.Commands.Jwt;
using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.CommandHandlers.Jwt
{
    [TestClass]
    public class GenerateTokenCommandHandlerTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestMethod]
        public async Task Handle_ValidCommand_ShouldReturnJwtToken()
        {
            //arrange
            var jwtSettings = _fixture.Build<JwtSettings>().Create();

            var settingsProviderMock = new Mock<IJwtSettingsProvider>();
            settingsProviderMock.Setup(p => p.Provide()).Returns(jwtSettings);

            var sut = new GenerateTokenCommandHandler(settingsProviderMock.Object);

            var command = new GenerateTokenCommand("test", new string[] { "test" });

            //act
            var token = await sut.Handle(command, default);

            var tokenHandler = new JwtSecurityTokenHandler();

            //assert
            Assert.AreNotEqual(string.Empty, tokenHandler.WriteToken(token));
        }
    }
}
