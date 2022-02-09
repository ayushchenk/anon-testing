using AnonTesting.BLL.CommandHandlers.User;
using AnonTesting.BLL.Commands.User;
using AnonTesting.BLL.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.CommandHandlers.User
{
    [TestClass]
    public class RegisterUserCommandHandlerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly RegisterUserCommandHandler _sut;

        public RegisterUserCommandHandlerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _sut = new RegisterUserCommandHandler(_mediatorMock.Object);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mediatorMock.Reset();
        }

        [TestMethod]
        public async Task Handle_CommandWithNewEmail_ShouldCreateAndLoginUser()
        {
            //arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), default))
                .ReturnsAsync(IdentityResult.Success);

            _mediatorMock.Setup(m => m.Send(It.IsAny<LoginUserCommand>(), default))
                .ReturnsAsync(Result.Success(new Token()));

            var command = new RegisterUserCommand("email", "password");

            //act
            var tokenResult = await _sut.Handle(command, default);

            //assert
            Assert.IsTrue(tokenResult.IsSuccess);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), default), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<LoginUserCommand>(), default), Times.Once);
        }

        [TestMethod]
        public async Task Handle_CommandWithExistingEmail_ShouldReturnResultWithError()
        {
            //arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), default))
                .ReturnsAsync(IdentityResult.Failed());

            var command = new RegisterUserCommand("email", "password");

            var tokenResult = await _sut.Handle(command, default);

            //act
            Assert.IsFalse(tokenResult.IsSuccess);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), default), Times.Once);
        }
    }
}
