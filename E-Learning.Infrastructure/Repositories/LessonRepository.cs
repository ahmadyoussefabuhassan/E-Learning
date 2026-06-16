using E_Learning.Domain.Lessons;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsyncByUnitAsync(Guid unitId, CancellationToken cancellationToken)
            => await _dbContext.Set<Lesson>()
            .AsNoTracking()
            .Where(u => u.UnitId == unitId)
            .ToListAsync(cancellationToken);
        public async override Task<Lesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<Lesson>()
            .AsNoTracking()
            .Include(u => u.Unit)
            .ThenInclude(s => s.Section)
            .ThenInclude(c => c.Course)
            .FirstOrDefaultAsync(l => l.Id == id , cancellationToken);
    }
}
