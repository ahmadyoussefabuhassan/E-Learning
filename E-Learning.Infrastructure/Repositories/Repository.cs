using E_Learning.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal  class Repository<T> : IRepository<T>  where T : Entity
    {
        protected readonly ApplicationDbContext _dbContext;
        protected Repository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            => await _dbContext.Set<T>().AddAsync(entity, cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity is not null)
            {
                _dbContext.Set<T>().Remove(entity);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _dbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);

        public IQueryable<T> GetAllQueryableAsync(CancellationToken cancellationToken = default)
            => _dbContext.Set<T>().AsNoTracking();

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<T>()
                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
