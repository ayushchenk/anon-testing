using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;
using AnonTesting.BLL.Services.Abstract;
using AnonTesting.DAL.Model;
using AnonTesting.DAL.Repositories.Abstract;
using AutoMapper;

namespace AnonTesting.BLL.Services
{
    public class AnswerService : GenericService<Answer, AnswerDto>, IAnswerService
    {
        public AnswerService(GenericRepository<Answer> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
