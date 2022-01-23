using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;
using AnonTesting.BLL.Services.Abstract;
using AnonTesting.DAL.Interfaces;
using AnonTesting.DAL.Model;
using AutoMapper;

namespace AnonTesting.BLL.Services
{
    public class TestResultService : GenericService<TestResult, TestResultDto>, ITestResultService
    {
        public TestResultService(IEntityRepository<TestResult> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
