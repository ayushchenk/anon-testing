namespace AnonTesting.BLL.Model
{
    public class CompletedQuestion
    {
        public Guid QuestionId { set; get; }
        public string? AnswerString { set; get; }
        public IEnumerable<Guid>? Answers { set; get; }
    }
}
