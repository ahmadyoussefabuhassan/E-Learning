using E_Learning.Domain.ExamVideos;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class ExamVideoRepository : Repository<ExamVideo>, IExamVideoRepository
    {
        public ExamVideoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ExamVideo>> GetAllByExamAsync(Guid ExamId, CancellationToken cancellation = default)
            => await _dbContext.Set<ExamVideo>()
            .AsNoTracking()
            .Where(ex => ex.ExamExplanationId == ExamId)
            .ToListAsync(cancellation);

        public override async Task<ExamVideo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            =>  await _dbContext.Set<ExamVideo>()
            .Include(ex => ex.ExamExplanation)
            .ThenInclude(c => c.Course)
            .FirstOrDefaultAsync(c => c.Id == id , cancellationToken);
    }
}
