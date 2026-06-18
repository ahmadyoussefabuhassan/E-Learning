using E_Learning.Domain.InvtensivesVideos;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class InvtensivesVideosRepositry : Repository<InvtensivesVideos>, IInvtensivesVideosRepositry
    {
        public InvtensivesVideosRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<InvtensivesVideos>> GetAllByInvtensiveAsync(Guid invtensiveId, CancellationToken cancellation)
            => await _dbContext.Set<InvtensivesVideos>()
            .AsNoTracking()
            .Where(inv => inv.InvtensiveId == invtensiveId)
            .ToListAsync(cancellation);

        public override async Task<InvtensivesVideos?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            =>  await _dbContext.Set<InvtensivesVideos>()
            .Include(inv => inv.Invtensive)
            .ThenInclude(c => c.Course)
            .FirstOrDefaultAsync(v => v.Id == id , cancellationToken);
    }
}
