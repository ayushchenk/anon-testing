using AnonTesting.BLL.Commands.Test;
using AnonTesting.BLL.Interfaces.Commands;
using AnonTesting.BLL.Model;
using AnonTesting.DAL.Model;
using AutoMapper;

namespace AnonTesting.BLL.CommandHandlers.Test
{
    public class ReviewTestCommandHandler : ICommandHandler<ReviewTestCommand, TestResultDto>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ReviewTestCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TestResultDto> Handle(ReviewTestCommand command, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<ReviewTestCommand, TestResultDto>(command);

            foreach (var completedQuestion in command.Questions)
            {
                var originalQuestion = await _context.Questions.FindAsync(completedQuestion.QuestionId);

                if (originalQuestion == null)
                {
                    continue;
                }

                if (IsAnswerCorrect(completedQuestion, originalQuestion))
                {
                    result.CorrectQuestions++;
                }
            }

            return result;
        }

        private bool IsAnswerCorrect(CompletedQuestion completedQuestion, Question originalQuestion)
        {
            //try
            //{
                switch (originalQuestion.QuestionType)
                {
                    case QuestionType.SingleAnswer: return CheckSingleAnswer(completedQuestion, originalQuestion);
                    case QuestionType.MultipleAnswers: return CheckMultipleAnswers(completedQuestion, originalQuestion);
                    case QuestionType.StringAnswer: return CheckStringAnswer(completedQuestion, originalQuestion);
                    default: return false;
                }
            //}
            //catch
            //{
            //    return false;
            //}
        }

        private bool CheckSingleAnswer(CompletedQuestion completedQuestion, Question originalQuestion)
        {
            var correctAnswer = originalQuestion.Answers.First(a => a.IsCorrect);

            return completedQuestion.Answers!.First() == correctAnswer.Id;
        }

        private bool CheckMultipleAnswers(CompletedQuestion completedQuestion, Question originalQuestion)
        {
            var correctAnswersIds = originalQuestion.Answers
                .Where(a => a.IsCorrect)
                .Select(a => a.Id)
                .OrderBy(a => a);

            return Enumerable.SequenceEqual(correctAnswersIds, completedQuestion.Answers!.OrderBy(a => a));
        }

        private bool CheckStringAnswer(CompletedQuestion completedQuestion, Question originalQuestion)
        {
            var answerString = originalQuestion.Answers.First().Content.Trim().ToLower();

            return completedQuestion.AnswerString?.Trim().ToLower() == answerString;
        }
    }
}
