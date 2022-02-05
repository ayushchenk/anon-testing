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
    public class ReviewMultipleAnswerQuestionCommandHandlerTests
    {
        private readonly DAL.Model.Question MultipleAnswerQuestion = new DAL.Model.Question()
        {
            Id = Guid.NewGuid(),
            Content = "multiple answers",
            QuestionType = QuestionType.MultipleAnswer,
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
                },
                new Answer()
                {
                    Id = Guid.NewGuid(),
                    Content = "answer3",
                    IsCorrect = true
                }
            }
        };

        private readonly ReviewMultipleAnswerQuestionCommandHandler _sut = new();

        [TestMethod]
        public async Task Handle_CorrectMultipleAnswerQuestion_ShouldReturnTrue()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = MultipleAnswerQuestion.Id,
                Answers = MultipleAnswerQuestion.Answers.Where(a => a.IsCorrect).Select(a => a.Id)
            };

            var command = new ReviewMultipleAnswerQuestionCommand(completedQuestion, MultipleAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Handle_OnlyOneCorrectAnswerOutOfTwoForMultipleAnswerQuestion_ShouldReturnFalse()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = MultipleAnswerQuestion.Id,
                Answers = new Guid[] { MultipleAnswerQuestion.Answers.First(a => a.IsCorrect).Id }
            };

            var command = new ReviewMultipleAnswerQuestionCommand(completedQuestion, MultipleAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Handle_ExtraWrongAnswerForMultipleAnswerQuestion_ShouldReturnFalse()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = MultipleAnswerQuestion.Id,
                Answers = MultipleAnswerQuestion.Answers.Select(a => a.Id)
            };

            var command = new ReviewMultipleAnswerQuestionCommand(completedQuestion, MultipleAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Handle_EmptyAnswersForMultipleAnswerQuestion_ShouldReturnFalse()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = MultipleAnswerQuestion.Id,
                Answers = new Guid[] { }
            };

            var command = new ReviewMultipleAnswerQuestionCommand(completedQuestion, MultipleAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Handle_NullAnswersForMultipleAnswerQuestion_ShouldReturnFalse()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = MultipleAnswerQuestion.Id,
                Answers = null
            };

            var command = new ReviewMultipleAnswerQuestionCommand(completedQuestion, MultipleAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
        }
    }
}
