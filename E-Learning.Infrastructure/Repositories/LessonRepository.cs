using E_Learning.Domain.Lessons;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Lesson>> GetLessonsAsyncByUnit(Guid unitId, CancellationToken cancellationToken)
            => await _dbContext.Set<Lesson>()
            .AsNoTracking()
            .Where(u => u.UnitId == unitId)
            .ToListAsync(cancellationToken);
    }
}
