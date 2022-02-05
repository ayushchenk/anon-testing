using AnonTesting.BLL.Model;
using AnonTesting.BLL.Validators.Question;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AnonTesting.BLL.Tests.Validators.Question
{
    [TestClass]
    public class QuestionDtoValidatorTests
    {
        private readonly QuestionDtoValidator _sut = new QuestionDtoValidator();

        [TestMethod]
        public void Validate_SingleAnswerQuestionWithOneCorrectAnswer_ShouldBeValid()
        {
            //arrage
            var question = new QuestionDto()
            {
                Content = "question",
                QuestionType = DAL.Model.QuestionType.SingleAnswer,
                Answers = new List<AnswerDto>()
                {
                    new AnswerDto()
                    {
                        Content = "answer1",
                        IsCorrect = false
                    },
                    new AnswerDto()
                    {
                        Content = "answer2",
                        IsCorrect = true
                    }
                }
            };

            //act
            var result = _sut.Validate(question);

            //assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Validate_SingleAnswerQuestionWithTwoCorrectAnswers_ShouldBeInvalid()
        {
            //arrage
            var question = new QuestionDto()
            {
                Content = "question",
                QuestionType = DAL.Model.QuestionType.SingleAnswer,
                Answers = new List<AnswerDto>()
                {
                    new AnswerDto()
                    {
                        Content = "answer1",
                        IsCorrect = true
                    },
                    new AnswerDto()
                    {
                        Content = "answer2",
                        IsCorrect = true
                    }
                }
            };

            //act
            var result = _sut.Validate(question);

            //assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Validate_SingleAnswerQuestionWithZeroCorrectAnswers_ShouldBeInvalid()
        {
            //arrage
            var question = new QuestionDto()
            {
                Content = "question",
                QuestionType = DAL.Model.QuestionType.SingleAnswer,
                Answers = new List<AnswerDto>()
                {
                    new AnswerDto()
                    {
                        Content = "answer1",
                        IsCorrect = false
                    },
                    new AnswerDto()
                    {
                        Content = "answer2",
                        IsCorrect = false
                    }
                }
            };

            //act
            var result = _sut.Validate(question);

            //assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Validate_MultipleAnswerQuestionWithOneCorrectAnswer_ShouldBeValid()
        {
            //arrage
            var question = new QuestionDto()
            {
                Content = "question",
                QuestionType = DAL.Model.QuestionType.MultipleAnswer,
                Answers = new List<AnswerDto>()
                {
                    new AnswerDto()
                    {
                        Content = "answer1",
                        IsCorrect = false
                    },
                    new AnswerDto()
                    {
                        Content = "answer2",
                        IsCorrect = true
                    }
                }
            };

            //act
            var result = _sut.Validate(question);

            //assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Validate_MultipleAnswerQuestionWithTwoCorrectAnswers_ShouldBeValid()
        {
            //arrage
            var question = new QuestionDto()
            {
                Content = "question",
                QuestionType = DAL.Model.QuestionType.MultipleAnswer,
                Answers = new List<AnswerDto>()
                {
                    new AnswerDto()
                    {
                        Content = "answer1",
                        IsCorrect = true
                    },
                    new AnswerDto()
                    {
                        Content = "answer2",
                        IsCorrect = true
                    }
                }
            };

            //act
            var result = _sut.Validate(question);

            //assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Validate_MultipleAnswerQuestionWithZeroCorrectAnswers_ShouldBeInvalid()
        {
            //arrage
            var question = new QuestionDto()
            {
                Content = "question",
                QuestionType = DAL.Model.QuestionType.MultipleAnswer,
                Answers = new List<AnswerDto>()
                {
                    new AnswerDto()
                    {
                        Content = "answer1",
                        IsCorrect = false
                    },
                    new AnswerDto()
                    {
                        Content = "answer2",
                        IsCorrect = false
                    }
                }
            };

            //act
            var result = _sut.Validate(question);

            //assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Validate_StringAnswerQuestionWithOneCorrectAnswer_ShouldBeValid()
        {
            //arrage
            var question = new QuestionDto()
            {
                Content = "question",
                QuestionType = DAL.Model.QuestionType.StringAnswer,
                Answers = new List<AnswerDto>()
                {
                    new AnswerDto()
                    {
                        Content = "answer1",
                        IsCorrect = true
                    }
                }
            };

            //act
            var result = _sut.Validate(question);

            //assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Validate_StringAnswerQuestionWithOneIncorrectAnswer_ShouldBeInvalid()
        {
            //arrage
            var question = new QuestionDto()
            {
                Content = "question",
                QuestionType = DAL.Model.QuestionType.StringAnswer,
                Answers = new List<AnswerDto>()
                {
                    new AnswerDto()
                    {
                        Content = "answer1",
                        IsCorrect = false
                    }
                }
            };

            //act
            var result = _sut.Validate(question);

            //assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Validate_StringAnswerQuestionWithTwoAnswersAndOneCorrect_ShouldBeInvalid()
        {
            //arrage
            var question = new QuestionDto()
            {
                Content = "question",
                QuestionType = DAL.Model.QuestionType.StringAnswer,
                Answers = new List<AnswerDto>()
                {
                    new AnswerDto()
                    {
                        Content = "answer1",
                        IsCorrect = false
                    },
                     new AnswerDto()
                    {
                        Content = "answer1",
                        IsCorrect = true
                    }
                }
            };

            //act
            var result = _sut.Validate(question);

            //assert
            Assert.IsFalse(result.IsValid);
        }
    }
}
