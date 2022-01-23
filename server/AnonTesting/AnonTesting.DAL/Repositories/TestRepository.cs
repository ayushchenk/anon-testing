using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AnonTesting.DAL.Repositories
{
    public class TestRepository : GenericRepository<Test>
    {
        public TestRepository(DbContext context) : base(context)
        { 
        }
    }
}
