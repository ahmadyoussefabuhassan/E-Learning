using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Students;

namespace E_Learning.Domain.StudentSubscription
{
    public interface IStudentSubscriptionRepositry : IRepository<StudentSubscription>
    {
        Task<List<Guid>> GetSubscribedStudentIdsAsync(Guid courseId, CancellationToken cancellation);
    }
}

