using AnonTesting.BLL.Interfaces;

namespace AnonTesting.BLL.Model
{
    public class TestDto : IDto
    {
        public Guid Id { set; get; }
        public string Title { set; get; } = null!;
        public string UserId { set; get; } = null!;
    }
}
