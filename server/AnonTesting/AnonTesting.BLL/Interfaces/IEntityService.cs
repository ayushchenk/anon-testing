using System.Linq.Expressions;

namespace AnonTesting.BLL.Interfaces
{
    public interface IEntityService<TDto> where TDto : IDto
    {
        ValueTask<TDto?> GetAsync(Guid id);
        ValueTask<IEnumerable<TDto>> GetAllAsync();
        ValueTask<IEnumerable<TDto>> FindByAsync(Expression<Func<TDto, bool>> predicate);
        ValueTask<Guid> CreateAsync(TDto dto);
        ValueTask BulkCreateAsync(IEnumerable<TDto> dtos);
        ValueTask<bool> UpdateAsync(TDto dto);
        ValueTask<bool> DeleteAsync(Guid id);
    }
}
