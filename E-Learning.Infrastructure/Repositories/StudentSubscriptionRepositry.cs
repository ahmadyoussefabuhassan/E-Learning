using E_Learning.Domain.StudentSubscription;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class StudentSubscriptionRepositry : Repository<StudentSubscription>, IStudentSubscriptionRepositry
    {
        public StudentSubscriptionRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Guid>> GetSubscribedStudentIdsAsync(Guid courseId, CancellationToken cancellation)
            => await _dbContext.Set<StudentSubscription>()
            .AsNoTracking()
            .Where(c => c.TargetId == courseId && c.Status == SubscriptionStatus.Completed)
            .Select(c => c.StudentId)
            .ToListAsync(cancellation);


    }
}
