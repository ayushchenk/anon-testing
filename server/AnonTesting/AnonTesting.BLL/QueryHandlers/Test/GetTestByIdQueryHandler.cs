using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;
using AnonTesting.BLL.Queries.Test;
using AnonTesting.DAL.Model;
using AutoMapper;

namespace AnonTesting.BLL.QueryHandlers.Test
{
    public class GetTestByIdQueryHandler : IQueryHandler<GetTestByIdQuery, TestDto?>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GetTestByIdQueryHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TestDto?> Handle(GetTestByIdQuery query, CancellationToken cancellationToken)
        {
            var test = await _context.Tests.FindAsync(query.Id);

            return test != null
                ? _mapper.Map<DAL.Model.Test, TestDto>(test)
                : null;
        }
    }
}
