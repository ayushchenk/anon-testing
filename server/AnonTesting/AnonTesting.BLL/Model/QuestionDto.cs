using AnonTesting.BLL.Interfaces;
using AnonTesting.DAL.Model;

namespace AnonTesting.BLL.Model
{
    public class QuestionDto : IDto
    {
        public Guid Id { set; get; }
        public QuestionType QuestionType { set; get; }
        public string Content { set; get; } = null!;
        public Guid TestId { set; get; }
    }
}
