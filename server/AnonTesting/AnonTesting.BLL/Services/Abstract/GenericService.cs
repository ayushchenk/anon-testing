using AnonTesting.BLL.Interfaces;
using AnonTesting.DAL.Interfaces;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using System.Linq.Expressions;

namespace AnonTesting.BLL.Services.Abstract
{
    public abstract class GenericService<TEntity, TDto> : IEntityService<TDto>, IDisposable
        where TEntity : class, IEntity
        where TDto : class, IDto
    {
        protected readonly IEntityRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IEntityRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual ValueTask BulkCreateAsync(IEnumerable<TDto> dtos)
        {
            var entities = dtos.Select(dto => _mapper.Map<TDto, TEntity>(dto));

            return _repository.BulkCreateAsync(entities); 
        }

        public virtual ValueTask<Guid> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TDto, TEntity>(dto);

            return _repository.CreateAsync(entity);
        }

        public virtual ValueTask<bool> DeleteAsync(Guid id)
        {
            return _repository.DeleteAsync(id);
        }

        public virtual async ValueTask<IEnumerable<TDto>> FindByAsync(Expression<Func<TDto, bool>> dtoPredicate)
        {
            var entityPredicate = _mapper.MapExpression<Expression<Func<TDto, bool>>, Expression<Func<TEntity, bool>>>(dtoPredicate);

            var entities = await _repository.FindByAsync(entityPredicate);

            return entities.Select(e => _mapper.Map<TEntity, TDto>(e));
        }

        public virtual async ValueTask<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            return entities.Select(e => _mapper.Map<TEntity, TDto>(e));
        }

        public async ValueTask<TDto?> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);

            return entity == null ? null : _mapper.Map<TEntity, TDto>(entity);
        }

        public virtual ValueTask<bool> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TDto, TEntity>(dto);

            return _repository.UpdateAsync(entity);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
