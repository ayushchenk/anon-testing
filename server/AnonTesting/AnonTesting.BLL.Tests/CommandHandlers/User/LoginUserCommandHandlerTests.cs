using AnonTesting.BLL.CommandHandlers.User;
using AnonTesting.BLL.Commands.Jwt;
using AnonTesting.BLL.Commands.User;
using AnonTesting.BLL.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.CommandHandlers.User
{
    [TestClass]
    public class LoginUserCommandHandlerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<UserManager<DAL.Model.User>> _userManagerMock;
        private readonly LoginUserCommandHandler _sut;

        public LoginUserCommandHandlerTests()
        {
            _mediatorMock = new Mock<IMediator>();

            var userStoreMock = new Mock<IUserStore<DAL.Model.User>>();
            _userManagerMock = new Mock<UserManager<DAL.Model.User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            _sut = new LoginUserCommandHandler(_userManagerMock.Object, _mediatorMock.Object);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mediatorMock.Reset();
            _userManagerMock.Reset();
        }

        [TestMethod]
        public async Task Handle_CommandWithExistingUserAndCorrectPassword_ShouldReturnToken()
        {
            //arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GenerateTokenCommand>(), default))
                .ReturnsAsync(new Token());

            var returnedUser = new DAL.Model.User();

            _userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(returnedUser);

            _userManagerMock.Setup(m => m.CheckPasswordAsync(returnedUser, It.IsAny<string>()))
                .ReturnsAsync(true);

            _userManagerMock.Setup(m => m.GetRolesAsync(returnedUser))
                .ReturnsAsync(new string[] { });

            var command = new LoginUserCommand("email", "password");

            //act
            var resultToken = await _sut.Handle(command, default);

            //assert
            _mediatorMock.Verify(m => m.Send(It.IsAny<GenerateTokenCommand>(), default), Times.Once);
            _userManagerMock.Verify(m => m.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _userManagerMock.Verify(m => m.CheckPasswordAsync(returnedUser, It.IsAny<string>()), Times.Once);
            _userManagerMock.Verify(m => m.GetRolesAsync(returnedUser), Times.Once);
            Assert.IsNotNull(resultToken);
        }

        [TestMethod]
        public async Task Handle_CommandWithExistingUserAndWrongPassword_ShouldThrowUserNotFoundException()
        {
            //arrange
            _userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new DAL.Model.User());

            _userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<DAL.Model.User>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var command = new LoginUserCommand("email", "password");

            //act
            await _sut.Handle(command, default);
        }

        [TestMethod]
        public async Task Handle_CommandWithNotExistingUser_ShouldThrowUserNotFoundException()
        {
#pragma warning disable CS8620

            //arrange
            _userManagerMock.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            var command = new LoginUserCommand("email", "password");

            //act
            await _sut.Handle(command, default);

#pragma warning restore CS8620
        }
    }
}
