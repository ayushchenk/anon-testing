using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;
using AnonTesting.BLL.Services.Abstract;
using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories.Abstract;
using AutoMapper;

namespace AnonTesting.BLL.Services
{
    public class QuestionService : GenericService<Question, QuestionDto>, IQuestionService
    {
        public QuestionService(GenericRepository<Question> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
