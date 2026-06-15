using E_Learning.Domain.Units;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class UnitRepository : Repository<Unit>, IUnitRepository
    {
        public UnitRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Unit>> GetAllBySectionAsync(Guid sectionId, CancellationToken cancellationToken)
            => await _dbContext.Set<Unit>()
            .AsNoTracking()
            .Where(s => s.SectionId == sectionId)
            .ToListAsync(cancellationToken);

        public override async Task<Unit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<Unit>()
            .Include(l => l.Lessons)
            .Include(s => s.Section)
            .ThenInclude(c => c.Course)
            .FirstOrDefaultAsync(s => s.Id == id , cancellationToken);
    }
}
