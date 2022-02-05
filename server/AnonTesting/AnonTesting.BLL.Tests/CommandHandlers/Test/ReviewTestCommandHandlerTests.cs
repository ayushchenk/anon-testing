using AnonTesting.BLL.CommandHandlers.Test;
using AnonTesting.BLL.Commands.Question;
using AnonTesting.BLL.Commands.Test;
using AnonTesting.BLL.Model;
using AnonTesting.DAL.Model;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.CommandHandlers.Test
{
    [TestClass]
    public class ReviewTestCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ReviewTestCommandHandler _sut;


        public ReviewTestCommandHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(m => m.Map<ReviewTestCommand, TestResultDto>(It.IsAny<ReviewTestCommand>()))
                .Returns(new TestResultDto());

            _mediatorMock = new Mock<IMediator>();

            _sut = new ReviewTestCommandHandler(_mapperMock.Object, _mediatorMock.Object);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mediatorMock.Reset();
        }

        [TestMethod]
        public async Task Handle_TestAttemptWithAllCorrectAnswers_ShuldMapAndReturnCorrectResult()
        {
            //arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ReviewQuestionCommand>(), default)).ReturnsAsync(true);
            _mapperMock.Setup(m => m.Map<ReviewTestCommand, TestResultDto>(It.IsAny<ReviewTestCommand>()))
                .Returns(new TestResultDto());

            var questions = new List<CompletedQuestion>() 
            { 
                new CompletedQuestion(),
                new CompletedQuestion(),
                new CompletedQuestion(),
                new CompletedQuestion()
            };

            var command = new ReviewTestCommand(Guid.NewGuid(), "me", questions);

            //act
            var testResult = await _sut.Handle(command, default);

            //assert
            Assert.AreEqual(questions.Count, testResult.CorrectQuestions);
            _mapperMock.Verify(m => m.Map<ReviewTestCommand, TestResultDto>(It.IsAny<ReviewTestCommand>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewQuestionCommand>(), default), Times.Exactly(questions.Count));
        }

        [TestMethod]
        public async Task Handle_TestAttemptWithAllWrongAnswers_ShuldMapAndReturnCorrectResult()
        {
            //arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<ReviewQuestionCommand>(), default)).ReturnsAsync(false);
            _mapperMock.Setup(m => m.Map<ReviewTestCommand, TestResultDto>(It.IsAny<ReviewTestCommand>()))
                .Returns(new TestResultDto());

            var questions = new List<CompletedQuestion>()
            {
                new CompletedQuestion(),
                new CompletedQuestion(),
                new CompletedQuestion(),
                new CompletedQuestion()
            };

            var command = new ReviewTestCommand(Guid.NewGuid(), "me", questions);

            //act
            var testResult = await _sut.Handle(command, default);

            //assert
            Assert.AreEqual(0, testResult.CorrectQuestions);
            _mapperMock.Verify(m => m.Map<ReviewTestCommand, TestResultDto>(It.IsAny<ReviewTestCommand>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewQuestionCommand>(), default), Times.Exactly(questions.Count));
        }
    }
}