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
    public class ReviewStringAnswerQuestionCommandHandlerTests
    {
        private readonly DAL.Model.Question StringAnswerQuestion = new DAL.Model.Question()
        {
            Id = Guid.NewGuid(),
            Content = "string answer",
            QuestionType = QuestionType.MultipleAnswer,
            Answers = new List<Answer>()
            {
                new Answer()
                {
                    Id = Guid.NewGuid(),
                    Content = "Answer",
                    IsCorrect = true
                }
            }
        };

        private readonly ReviewStringAnswerQuestionCommandHandler _sut = new();

        [TestMethod]
        public async Task Handle_CorrectStringAnswerQuestion_ShouldReturnTrue()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = StringAnswerQuestion.Id,
                AnswerString = StringAnswerQuestion.Answers.First().Content.ToLower()
            };

            var command = new ReviewStringAnswerQuestionCommand(completedQuestion, StringAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Handle_IncorrectStringAnswerQuestion_ShouldReturnFalse()
        {
            //arrange
            var completedQuestion = new CompletedQuestion()
            {
                QuestionId = StringAnswerQuestion.Id,
                AnswerString = string.Empty
            };

            var command = new ReviewStringAnswerQuestionCommand(completedQuestion, StringAnswerQuestion);

            //act
            var result = await _sut.Handle(command, default);

            //assert
            Assert.IsFalse(result);
        }
    }
}
