using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AnonTesting.DAL.Repositories
{
    public class TestResultRepository : IEntityRepository<TestResult>
    {
        public TestResultRepository(ApplicationContext context) : base(context)
        { 
        }
    }
}
