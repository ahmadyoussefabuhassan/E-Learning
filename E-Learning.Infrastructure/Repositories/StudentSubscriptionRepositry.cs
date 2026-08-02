using E_Learning.Domain.Courses;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.Sections;
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
        public async Task<bool> IsAlreadySubscribedAsync(Guid studentId, Guid targetId, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<StudentSubscription>()
                .AnyAsync(s => s.StudentId == studentId &&
                               s.TargetId == targetId &&
                               s.Status != SubscriptionStatus.Rejected, 
                          cancellationToken
                );
        }

        public override async Task<StudentSubscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<StudentSubscription>()
            .Include(s => s.Students)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync(sp => sp.Id == id, cancellationToken);

        public async Task<IEnumerable<Course>> GetAllCourseSubscribersAsync(Guid studentId, CancellationToken cancellation)
        {
            var subscriptions = await _dbContext.Set<StudentSubscription>()
                 .AsNoTracking()
                 .Where(s => s.StudentId == studentId && s.Status == SubscriptionStatus.Completed)
                 .ToListAsync(cancellation);
            if (subscriptions == null || !subscriptions.Any())
                return Enumerable.Empty<Course>();
            var targetIds = subscriptions
                     .Where(s => s.TargetType != null && (s.TargetType.Value == "كورس" || s.TargetType.Value == "Course"))
                     .Select(s => s.TargetId)
                      .ToList();

            if (targetIds == null || !targetIds.Any())
                return Enumerable.Empty<Course>();
            return await _dbContext.Set<Course>()
                           .Include(c => c.Classes)
                           .Include(c => c.Teachers)
                            .Where(c => targetIds.Contains(c.Id)) 
                            .AsNoTracking()
                            .ToListAsync(cancellation);
        }

        public async Task<IEnumerable<Section>> GetAllSectionSubscribersAsync(Guid studentId, CancellationToken cancellation)
        {
            var subscriptions = await _dbContext.Set<StudentSubscription>()
                 .AsNoTracking()
                 .Where(s => s.StudentId == studentId && s.Status == SubscriptionStatus.Completed)
                 .ToListAsync(cancellation);
            if(subscriptions == null || !subscriptions.Any())
                return Enumerable.Empty<Section>();

            var targetIds = subscriptions
             .Where(s => s.TargetType != null && (s.TargetType.Value == "قسم" || s.TargetType.Value == "Section"))
             .Select(s => s.TargetId)
             .ToList();

            if (!targetIds.Any()) return Enumerable.Empty<Section>();

            return await _dbContext.Set<Section>()
                        .Where(s => targetIds.Contains(s.Id))
                        .AsNoTracking()
                        .ToListAsync(cancellation);
        }

        public async Task<IEnumerable<Invtensives>> GetAllInvtensivesSubscribersAsync(Guid studentId, CancellationToken cancellation)
        {
            var subscriptions = await _dbContext.Set<StudentSubscription>()
                     .AsNoTracking()
                     .Where(s => s.StudentId == studentId && s.Status == SubscriptionStatus.Completed)
                     .ToListAsync(cancellation);
            if (subscriptions == null || !subscriptions.Any())
                return Enumerable.Empty<Invtensives>();
            var targetIds = subscriptions
                     .Where(s => s.TargetType != null && (s.TargetType.Value == "مكثفة" || s.TargetType.Value == "Invtensive"))
                    .Select(s => s.TargetId)
                    .ToList();

            if (!targetIds.Any())
                return Enumerable.Empty<Invtensives>();
            return await _dbContext.Set<Invtensives>()
                  .Where(i => targetIds.Contains(i.Id))
                .AsNoTracking()
                .ToListAsync(cancellation);
        }

        public async Task<IEnumerable<ExamExplanation>> GetAllExamExplanationSubscribersAsync(Guid studentId, CancellationToken cancellation) 
        {
            var subscriptions = await _dbContext.Set<StudentSubscription>()
                   .AsNoTracking()
                   .Where(s => s.StudentId == studentId && s.Status == SubscriptionStatus.Completed)
                   .ToListAsync(cancellation);
            if (subscriptions == null || !subscriptions.Any())
                return Enumerable.Empty<ExamExplanation>();
            var targetIds = subscriptions
                .Where(s => s.TargetType != null && (s.TargetType.Value == "دورة" || s.TargetType.Value == "ExamExplanation"))
                .Select(s => s.TargetId)
                .ToList();
            if (!targetIds.Any())
                return Enumerable.Empty<ExamExplanation>();
            return await _dbContext.Set<ExamExplanation>()
               .Where(e => targetIds.Contains(e.Id))
             .AsNoTracking()
             .ToListAsync(cancellation);
        }   
    }
}
