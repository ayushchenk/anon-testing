using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnonTesting.DAL.Interfaces
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity
    {
        ValueTask<TEntity?> GetAsync(Guid id);
        ValueTask<IEnumerable<TEntity>> GetAllAsync();
        ValueTask<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        ValueTask<Guid> CreateAsync(TEntity entity);
        ValueTask BulkCreateAsync(IEnumerable<TEntity> entities);
        ValueTask<bool> UpdateAsync(TEntity entity);
        ValueTask<bool> DeleteAsync(Guid id);
    }
}
