using E_Learning.Domain.ExamExplanations;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class ExamExplanationRepository : Repository<ExamExplanation>, IExamExplanationRepository
    {
        public ExamExplanationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ExamExplanation>> GetAllByCourseAsync(Guid courseId, CancellationToken cancellationToken)
            => await _dbContext.Set<ExamExplanation>()
            .AsNoTracking()
            .Where(c => c.CourseId == courseId)
            .ToListAsync(cancellationToken);

        public override async Task<ExamExplanation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<ExamExplanation>()
            .Include(c => c.Course)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    }

}