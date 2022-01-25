using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;
using AnonTesting.BLL.Queries.Test;
using AnonTesting.DAL.Interfaces;
using AutoMapper;

namespace AnonTesting.BLL.QueryHandlers.Test
{
    public class GetUserTestsQueryHandler : IQueryHandler<GetUserTestsQuery, IEnumerable<TestDto>>
    {
        private readonly IEntityRepository<DAL.Model.Test> _repository;
        private readonly IMapper _mapper;

        public GetUserTestsQueryHandler(IEntityRepository<DAL.Model.Test> testRepository, IMapper mapper)
        {
            _repository = testRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TestDto>> Handle(GetUserTestsQuery query, CancellationToken cancellationToken)
        {
            var userTests = await _repository.FindByAsync(test => test.UserId == query.UserId);

            return userTests.Select(test => _mapper.Map<DAL.Model.Test, TestDto>(test));
        }
    }
}
