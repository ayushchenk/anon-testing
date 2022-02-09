using AnonTesting.BLL.CommandHandlers.Jwt;
using AnonTesting.BLL.Commands.Jwt;
using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;
using AutoFixture;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.CommandHandlers.Jwt
{
    [TestClass]
    public class GenerateTokenCommandHandlerTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestMethod]
        public async Task Handle_ValidCommand_ShouldReturnToken()
        {
            //arrange
            var jwtSettings = _fixture.Build<JwtSettings>().Create();
            var user = _fixture.Build<DAL.Model.User>()
                .With(u => u.CreatedTests, value: null)
                .Create();

            var settingsProviderMock = new Mock<IJwtSettingsProvider>();
            settingsProviderMock.Setup(p => p.Provide()).Returns(jwtSettings);

            var securityTokenHandlerMock = new Mock<SecurityTokenHandler>();
            securityTokenHandlerMock.Setup(h => h.WriteToken(It.IsAny<SecurityToken>())).Returns("token");

            var sut = new GenerateTokenCommandHandler(settingsProviderMock.Object, securityTokenHandlerMock.Object);

            var command = new GenerateTokenCommand(user, new string[] { "test" });

            //act
            var token = await sut.Handle(command, default);

            //assert
            Assert.IsNotNull(token);
            settingsProviderMock.Verify(p => p.Provide(), Times.Once);
            securityTokenHandlerMock.Verify(h => h.WriteToken(It.IsAny<SecurityToken>()), Times.Once);
        }
    }
}
