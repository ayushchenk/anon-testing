using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AnonTesting.DAL.Repositories
{
    public class QuestionRepository : GenericRepository<Question>
    {
        public QuestionRepository(DbContext context) : base(context) 
        { 
        }
    }
}
