using AnonTesting.BLL.CommandHandlers.Test;
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
using System.Threading;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.CommandHandlers.Test
{
    [TestClass]
    public class CompleteTestCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<DbSet<DAL.Model.TestResult>> _testResultDbSetMock;
        private readonly Mock<ApplicationContext> _contextMock;
        private readonly CompleteTestCommandHandler _sut;

        public CompleteTestCommandHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(m => m.Map<TestResultDto, DAL.Model.TestResult>(It.IsAny<TestResultDto>()))
                .Returns<TestResultDto>(dto => new DAL.Model.TestResult()
                {
                    CompletedOn = dto.CompletedOn,
                    ContestantName = dto.ContestantName,
                    TestId = dto.TestId,
                    CorrectQuestions = dto.CorrectQuestions
                });

            _mediatorMock = new Mock<IMediator>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<ReviewTestCommand>(), default))
                .Returns<ReviewTestCommand, CancellationToken>((command, token) =>
                {
                    var result = new TestResultDto()
                    {
                        TestId = command.TestId,
                        ContestantName = command.ContestantName,
                        CorrectQuestions = 1,
                        CompletedOn = DateTime.Now
                    };

                    return Task.FromResult(result);
                });

            _testResultDbSetMock = new Mock<DbSet<DAL.Model.TestResult>>();
            _testResultDbSetMock.Setup(s => s.Add(It.IsAny<DAL.Model.TestResult>()))
                .Callback<DAL.Model.TestResult>(result =>
                {
                    result.Id = Guid.NewGuid();
                });

            _contextMock = new Mock<ApplicationContext>();
            _contextMock.Setup(c => c.TestResults).Returns(_testResultDbSetMock.Object);
            _contextMock.Setup(c => c.SaveChangesAsync(default));

            _sut = new CompleteTestCommandHandler(_mapperMock.Object, _mediatorMock.Object, _contextMock.Object);
        }

        [TestMethod]
        public async Task Handle_Command_ShouldReviewAndCreateResults()
        {
            //arrange
            var completedQuestions = new List<CompletedQuestion>() { new CompletedQuestion() };
            var command = new CompleteTestCommand(Guid.NewGuid(), "me", completedQuestions);

            //act
            var resultDto = await _sut.Handle(command, default);

            //assert
            Assert.AreNotEqual(Guid.Empty, resultDto.Id);
            _mapperMock.Verify(m => m.Map<TestResultDto, DAL.Model.TestResult>(It.IsAny<TestResultDto>()), Times.Once);
            _mediatorMock.Verify(m => m.Send(It.IsAny<ReviewTestCommand>(), default), Times.Once);
            _testResultDbSetMock.Verify(s => s.Add(It.IsAny<DAL.Model.TestResult>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
    }
}
