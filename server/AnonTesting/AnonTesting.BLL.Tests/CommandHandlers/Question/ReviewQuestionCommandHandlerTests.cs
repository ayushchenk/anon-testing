using AnonTesting.BLL.CommandHandlers.Question;
using AnonTesting.BLL.Commands.Question;
using AnonTesting.BLL.Model;
using AnonTesting.DAL.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.CommandHandlers.Question
{
    [TestClass]
    public class ReviewQuestionCommandHandlerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<DbSet<DAL.Model.Question>> _questionDbSetMock;
        private readonly Mock<ApplicationContext> _contextMock;
        private readonly ReviewQuestionCommandHandler _sut;

        public ReviewQuestionCommandHandlerTests()
        {
            _mediatorMock = new Mock<IMediator>();

            _questionDbSetMock = new Mock<DbSet<DAL.Model.Question>>();

            _contextMock = new Mock<ApplicationContext>();
            _contextMock.Setup(c => c.Questions).Returns(_questionDbSetMock.Object);

            _sut = new ReviewQuestionCommandHandler(_contextMock.Object, _mediatorMock.Object);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mediatorMock.Reset();
            _questionDbSetMock.Reset();
        }

        [TestMethod]
        public async Task Handle_CommandToSingleAnswerQuestion_ShouldSendReviewSingleAnswerQuestionCommand()
        {
            //arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ReviewSingleAnswerQuestionCommand>(), default)).ReturnsAsync(true);
            _questionDbSetMock.Setup(d => d.FindAsync(It.IsAny<Guid>())).ReturnsAsync(new DAL.Model.Question()
            {
                QuestionType = QuestionType.SingleAnswer
            });

            var command = new ReviewQuestionCommand(new CompletedQuestion());

            //act
            var result = await _sut.Handle(command, default);

            //assert
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewSingleAnswerQuestionCommand>(), default), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewStringAnswerQuestionCommand>(), default), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewMultipleAnswerQuestionCommand>(), default), Times.Never);
        }

        [TestMethod]
        public async Task Handle_CommandToStringAnswerQuestion_ShouldSendReviewStringAnswerQuestionCommand()
        {
            //arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ReviewSingleAnswerQuestionCommand>(), default)).ReturnsAsync(true);
            _questionDbSetMock.Setup(d => d.FindAsync(It.IsAny<Guid>())).ReturnsAsync(new DAL.Model.Question()
            {
                QuestionType = QuestionType.StringAnswer
            });

            var command = new ReviewQuestionCommand(new CompletedQuestion());

            //act
            var result = await _sut.Handle(command, default);

            //assert
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewSingleAnswerQuestionCommand>(), default), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewStringAnswerQuestionCommand>(), default), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewMultipleAnswerQuestionCommand>(), default), Times.Never);
        }

        [TestMethod]
        public async Task Handle_CommandToMultipleAnswerQuestion_ShouldSendReviewMultipleAnswerQuestionCommand()
        {
            //arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ReviewSingleAnswerQuestionCommand>(), default)).ReturnsAsync(true);
            _questionDbSetMock.Setup(d => d.FindAsync(It.IsAny<Guid>())).ReturnsAsync(new DAL.Model.Question()
            {
                QuestionType = QuestionType.MultipleAnswer
            });

            var command = new ReviewQuestionCommand(new CompletedQuestion());

            //act
            var result = await _sut.Handle(command, default);

            //assert
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewSingleAnswerQuestionCommand>(), default), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewStringAnswerQuestionCommand>(), default), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewMultipleAnswerQuestionCommand>(), default), Times.Once);
        }

        [TestMethod]
        public async Task Handle_CommandToUnknownQuestionType_ShouldReturnFalse()
        {
            //arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ReviewSingleAnswerQuestionCommand>(), default)).ReturnsAsync(true);
            _questionDbSetMock.Setup(d => d.FindAsync(It.IsAny<Guid>())).ReturnsAsync(new DAL.Model.Question()
            {
                QuestionType = (QuestionType)100
            });

            var command = new ReviewQuestionCommand(new CompletedQuestion());

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewSingleAnswerQuestionCommand>(), default), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewStringAnswerQuestionCommand>(), default), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewMultipleAnswerQuestionCommand>(), default), Times.Never);
        }

        [TestMethod]
        public async Task Handle_CommandToNotExistingQuestion_ShouldReturnFalse()
        {
            //arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ReviewSingleAnswerQuestionCommand>(), default)).ReturnsAsync(true);
            _questionDbSetMock.Setup(d => d.FindAsync(It.IsAny<Guid>())).Returns(ValueTask.FromResult<DAL.Model.Question?>(null));

            var command = new ReviewQuestionCommand(new CompletedQuestion());

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewSingleAnswerQuestionCommand>(), default), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewStringAnswerQuestionCommand>(), default), Times.Never);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewMultipleAnswerQuestionCommand>(), default), Times.Never);
        }
    }
}
