using E_Learning.Domain.Invtensives;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class InvtensivesRepositry : Repository<Invtensives>, IInvtensivesRepositry
    {
        public InvtensivesRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Invtensives>> GetAllInvtensivesByCourseAsync(Guid courseId, CancellationToken cancellationToken)
            => await _dbContext.Set<Invtensives>()
            .AsNoTracking()
            .Where(c => c.CourseID == courseId)
            .ToListAsync(cancellationToken);

        public override async Task<Invtensives?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<Invtensives>()
            .Include(c => c.Course)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}
