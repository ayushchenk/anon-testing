namespace AnonTesting.BLL.Model
{
    public class Token
    {
        public string Value { set; get; } = null!;
        public DateTime ExpriesOn { set; get; }
        public Guid UserId { set; get; }
    }
}
