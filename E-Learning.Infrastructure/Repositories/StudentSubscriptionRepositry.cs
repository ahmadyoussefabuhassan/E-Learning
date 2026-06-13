using E_Learning.Domain.Students;
using E_Learning.Domain.StudentSubscription;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class StudentSubscriptionRepositry : Repository<StudentSubscription>, IStudentSubscriptionRepositry
    {
        public StudentSubscriptionRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Guid>> GetSectionOrCourseSubscribersAsync(Guid sectionId, Guid courseId, CancellationToken cancellation)
            => await _dbContext.Set<StudentSubscription>()
                    .AsNoTracking()
                   .Where(s => s.Status == SubscriptionStatus.Completed &&
                   ((s.TargetId == sectionId ) ||
                    (s.TargetId == courseId )))
                    .Select(s => s.StudentId)
                    .Distinct() 
                    .ToListAsync(cancellation);

        public async Task<List<Guid>> GetSubscribedStudentIdsAsync(Guid courseId, CancellationToken cancellation)
            => await _dbContext.Set<StudentSubscription>()
            .AsNoTracking()
            .Where(c => c.TargetId == courseId && c.Status == SubscriptionStatus.Completed)
            .Select(c => c.StudentId)
            .ToListAsync(cancellation);

        public async Task<List<Guid>> GetAllStudentIdsAsync(CancellationToken cancellation)
            => await _dbContext.Set<Student>()
                    .Select(s => s.Id)
                    .ToListAsync(cancellation);


    }
}
