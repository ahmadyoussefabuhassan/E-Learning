using E_Learning.Domain.Sections;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Section?>> GetAllByCourseAsync(Guid courseId, CancellationToken cancellationToken)
            => await _dbContext.Set<Section>()
                .AsNoTracking()
                .Where(s => s.CourseId == courseId)
                .ToListAsync(cancellationToken);


        public override async Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<Section>()
            .Include(s => s.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<bool> HasRelatedDataAsync(Guid sectionId, CancellationToken cancellationToken)
            => await _dbContext.Set<Section>()
            .AnyAsync(s => s.Id != sectionId , cancellationToken);
    }
}
