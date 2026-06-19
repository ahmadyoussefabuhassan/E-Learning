using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Notification
{
    public interface INotificationRepositry : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
