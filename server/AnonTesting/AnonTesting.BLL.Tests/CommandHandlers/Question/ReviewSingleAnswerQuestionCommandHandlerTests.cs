using AnonTesting.BLL.CommandHandlers.Question;
using AnonTesting.BLL.Commands.Question;
using AnonTesting.BLL.Model;
using AnonTesting.DAL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnonTesting.BLL.Tests.CommandHandlers.Question
{
    [TestClass]
    public class ReviewSingleAnswerQuestionCommandHandlerTests
    {
        private readonly DAL.Model.Question SingleAnswerQuestion = new DAL.Model.Question()
        {
            Id = Guid.NewGuid(),
            Content = "single answer",
            QuestionType = QuestionType.SingleAnswer,
            Answers = new List<Answer>()
            {
                new Answer()
                {
                    Id = Guid.NewGuid(),
                    Content = "answer1",
                    IsCorrect = true
                },
                new Answer()
                {
                    Id = Guid.NewGuid(),
                    Content = "answer2",
                    IsCorrect = false
                }
            }
        };

        private readonly ReviewSingleAnswerQuestionCommandHandler _sut = new();

        [TestMethod]
        public async Task Handle_CorrectSingleAnswerQuestion_ShouldReturnTrue()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = SingleAnswerQuestion.Id,
                Answers = new[] { SingleAnswerQuestion.Answers.First(a => a.IsCorrect).Id }
            };

            var command = new ReviewSingleAnswerQuestionCommand(completedQuestion, SingleAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Handle_IncorrectSingleAnswerQuestion_ShouldReturnFalse()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = SingleAnswerQuestion.Id,
                Answers = new[] { SingleAnswerQuestion.Answers.First(a => !a.IsCorrect).Id }
            };

            var command = new ReviewSingleAnswerQuestionCommand(completedQuestion, SingleAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Handle_NotExistingAnswerForSingleAnswerQuestion_ShouldReturnResultWithZeroCorrectAnswer()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = SingleAnswerQuestion.Id,
                Answers = new[] { Guid.NewGuid() }
            };

            var command = new ReviewSingleAnswerQuestionCommand(completedQuestion, SingleAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Handle_EmptyAnswerForSingleAnswerQuestion_ShouldReturnFalse()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = SingleAnswerQuestion.Id,
                Answers = new Guid[] { }
            };

            var command = new ReviewSingleAnswerQuestionCommand(completedQuestion, SingleAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Handle_NullAnswerForSingleAnswerQuestion_ShouldReturnFalse()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = SingleAnswerQuestion.Id,
                Answers = null
            };

            var command = new ReviewSingleAnswerQuestionCommand(completedQuestion, SingleAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
        }
    }
}
