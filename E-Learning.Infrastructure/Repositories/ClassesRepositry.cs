using E_Learning.Domain.Classes;
using E_Learning.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class ClassesRepositry : Repository<Classes>, IClassesRepositry
    {
        public ClassesRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Classes?> GetClassesByNameAsync(ClassesName name, CancellationToken cancellationToken)
            => await _dbContext.Set<Classes>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);

        public async Task<bool> HasRelatedDataAsync(Guid classId, CancellationToken cancellationToken)
            => await _dbContext.Set<Course>()
            .AnyAsync(c => c.Id == classId, cancellationToken);

        public async Task<Classes?> IsClassesUniqueAsync(ClassesName name, CancellationToken cancellationToken)
            => await _dbContext.Set<Classes>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }
}
