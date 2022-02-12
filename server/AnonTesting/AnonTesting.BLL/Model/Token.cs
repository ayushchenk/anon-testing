namespace AnonTesting.BLL.Model
{
    public class Token
    {
        public string Value { set; get; } = null!;
        public DateTime ExpiresOn { set; get; }
        public Guid UserId { set; get; }
    }
}
