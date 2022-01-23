using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AnonTesting.DAL.Repositories
{
    public class AnswerRepository : IEntityRepository<Answer>
    {
        public AnswerRepository(ApplicationContext context) : base(context) 
        {
        }
    }
}
