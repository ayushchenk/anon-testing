using AnonTesting.BLL.Interfaces;

namespace AnonTesting.BLL.Model
{
    public class TestDto : IDto
    {
        public Guid Id { set; get; }
        public string Title { set; get; } = null!;
        public Guid UserId { set; get; }
        public IEnumerable<QuestionDto> Questions { set; get; } = null!;
    }
}
