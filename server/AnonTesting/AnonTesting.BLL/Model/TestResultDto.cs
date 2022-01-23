using AnonTesting.BLL.Interfaces;

namespace AnonTesting.BLL.Model
{
    public class TestResultDto : IDto
    {
        public Guid Id { set; get; }
        public int CorrectQuestions { set; get; }
        public string ContestantName { set; get; } = null!;
        public DateTime CompletedOn { set; get; }
        public Guid TestId { set; get; }
    }
}
