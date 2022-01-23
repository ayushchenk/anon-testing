using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AnonTesting.DAL.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>
    {
        public AnswerRepository(DbContext context) : base(context) 
        {
        }
    }
}
