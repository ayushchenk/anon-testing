using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;

namespace AnonTesting.BLL.Queries.Test
{
    public class GetTestByIdQuery : IQuery<TestDto?>
    {
        public Guid Id { get; }

        public GetTestByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
