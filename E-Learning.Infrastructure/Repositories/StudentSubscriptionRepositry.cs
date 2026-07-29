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
            => await (from sub in _dbContext.Set<StudentSubscription>()
                      join course in _dbContext.Set<Course>() on sub.TargetId equals course.Id
                      where sub.StudentId == studentId &&
                            sub.Status == SubscriptionStatus.Completed &&
                           ( sub.TargetType.Value == TargetTypes.Course.ToArabicString() || sub.TargetType.Value == TargetTypes.Course.ToString())
                      select course)
                  .Include(c => c.Classes)   
                  .Include(c => c.Teachers)
                  .AsNoTracking()
                  .ToListAsync(cancellation);

        public async Task<IEnumerable<Section>> GetAllSectionSubscribersAsync(Guid studentId, CancellationToken cancellation)
            => await (from sub in _dbContext.Set<StudentSubscription>()
                      join section in _dbContext.Set<Section>() on sub.TargetId equals section.Id
                      where sub.StudentId == studentId &&
                            sub.Status == SubscriptionStatus.Completed &&
                            (sub.TargetType.Value == TargetTypes.Section.ToArabicString() || sub.TargetType.Value == TargetTypes.Section.ToString())
                      select section)
                  .Include(s => s.Course)
                  .AsNoTracking()
                  .ToListAsync(cancellation);

        public async Task<IEnumerable<Invtensives>> GetAllInvtensivesSubscribersAsync(Guid studentId, CancellationToken cancellation)
            => await (from sub in _dbContext.Set<StudentSubscription>()
                      join inv in _dbContext.Set<Invtensives>() on sub.TargetId equals inv.Id
                      where sub.StudentId == studentId &&
                            sub.Status == SubscriptionStatus.Completed &&
                      (sub.TargetType.Value == TargetTypes.Invtensive.ToArabicString()|| sub.TargetType.Value == TargetTypes.Invtensive.ToString())
                      select inv)
                  .AsNoTracking()
                  .ToListAsync(cancellation);

        public async Task<IEnumerable<ExamExplanation>> GetAllExamExplanationSubscribersAsync(Guid studentId, CancellationToken cancellation)
            => await (from sub in _dbContext.Set<StudentSubscription>()
                      join exam in _dbContext.Set<ExamExplanation>() on sub.TargetId equals exam.Id
                      where sub.StudentId == studentId &&
                            sub.Status == SubscriptionStatus.Completed &&
                      (sub.TargetType.Value == TargetTypes.ExamExplanation.ToArabicString() || sub.TargetType.Value == TargetTypes.ExamExplanation.ToString())
                      select exam)
                  .AsNoTracking()
                  .ToListAsync(cancellation);
    }
}
