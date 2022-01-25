using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AnonTesting.DAL.Repositories
{
    public class TestRepository : EntityRepository<Test>
    {
        public TestRepository(ApplicationContext context) : base(context)
        { 
        }
    }
}
