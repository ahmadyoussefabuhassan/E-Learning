using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.Sections;
using E_Learning.Domain.Students;

namespace E_Learning.Domain.StudentSubscription
{
    public interface IStudentSubscriptionRepositry : IRepository<StudentSubscription>
    {
        Task<List<Guid>> GetSubscribedStudentIdsAsync(Guid courseId, CancellationToken cancellation);
        Task<List<Guid>> GetSectionOrCourseSubscribersAsync(Guid sectionId, Guid courseId, CancellationToken cancellation);
        Task<List<Guid>> GetAllStudentIdsAsync(CancellationToken cancellation);
        Task<bool> IsAlreadySubscribedAsync(Guid studentId, Guid targetId, CancellationToken cancellationToken);
        Task<IEnumerable<Course>> GetAllCourseSubscribersAsync(Guid studentId, CancellationToken cancellation);
        Task<IEnumerable<Section>> GetAllSectionSubscribersAsync(Guid studentId, CancellationToken cancellation);
        Task<IEnumerable<Invtensives.Invtensives>> GetAllInvtensivesSubscribersAsync(Guid studentId, CancellationToken cancellation);
        Task<IEnumerable<ExamExplanation>> GetAllExamExplanationSubscribersAsync(Guid studentId, CancellationToken cancellation);
    }
}

