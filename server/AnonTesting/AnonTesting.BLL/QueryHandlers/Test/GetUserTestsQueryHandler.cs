using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;
using AnonTesting.BLL.Queries.Test;
using AnonTesting.DAL.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AnonTesting.BLL.QueryHandlers.Test
{
    public class GetUserTestsQueryHandler : IQueryHandler<GetUserTestsQuery, IEnumerable<TestDto>>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GetUserTestsQueryHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IEnumerable<TestDto>> Handle(GetUserTestsQuery query, CancellationToken cancellationToken)
        {
            var userTestsEntities = _context.Tests.AsNoTracking().Where(test => test.UserId == query.UserId);

            var userTestsDtos = userTestsEntities.Select(test => _mapper.Map<DAL.Model.Test, TestDto>(test)).AsEnumerable();

            return Task.FromResult(userTestsDtos);
        }
    }
}
