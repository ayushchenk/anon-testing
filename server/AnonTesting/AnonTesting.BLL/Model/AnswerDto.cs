using AnonTesting.BLL.Interfaces;

namespace AnonTesting.BLL.Model
{
    public class AnswerDto : IDto
    {
        public Guid Id { set; get; }
        public bool IsCorrent { set; get; }
        public string Content { set; get; } = null!;
        public Guid QuestionId { set; get; }
        public Guid TestId { set; get; }
    }
}
