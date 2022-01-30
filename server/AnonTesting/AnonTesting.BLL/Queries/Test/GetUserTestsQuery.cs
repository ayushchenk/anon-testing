using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Queries.Test
{
    public class GetUserTestsQuery: IQuery<IEnumerable<TestDto>>
    {
        public Guid UserId { get; }

        public GetUserTestsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
