using AnonTesting.DAL.Interfaces;
using AnonTesting.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AnonTesting.DAL.Repositories.Abstract
{
    public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbSet<TEntity> _set;
        protected readonly DbContext _context;

        public EntityRepository(ApplicationContext context)
        {
            _set = context.Set<TEntity>();
            _context = context;
        }

        public async ValueTask<TEntity?> GetAsync(Guid id)
        {
            return await _set.FindAsync(id);
        }

        public async ValueTask<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _set.AsNoTracking().ToListAsync();
        }

        public async ValueTask<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _set.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async virtual ValueTask<Guid> CreateAsync(TEntity entity)
        {
            var entry = _set.Add(entity);

            await _context.SaveChangesAsync();

            return entry.Entity.Id;
        }

        public async virtual ValueTask BulkCreateAsync(IEnumerable<TEntity> entities)
        {
            _set.AddRange(entities);

            await _context.SaveChangesAsync();
        }

        public async ValueTask<bool> UpdateAsync(TEntity entity)
        {
            var notUpdatedEntity = await GetAsync(entity.Id);

            if (notUpdatedEntity != null)
            {
                _context.Entry(notUpdatedEntity).State = EntityState.Detached;

                _set.Update(entity);

                await _context.SaveChangesAsync();
            }

            return notUpdatedEntity != null;
        }

        public async ValueTask<bool> DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);

            if(entity != null)
            {
                _set.Remove(entity);

                await _context.SaveChangesAsync();
            }

            return entity != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
