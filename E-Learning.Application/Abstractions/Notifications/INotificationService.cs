

namespace E_Learning.Application.Abstractions.Notifications
{
    public interface INotificationService
    {
        Task SendToUserAsync(Guid userId, string title, string message, CancellationToken cancellation = default);
        Task SendToAllAsync(string title, string message, CancellationToken cancellation = default);
        Task SendToGroupAsync(string groupName, string title, string message, CancellationToken cancellation = default);
        Task SendToUsersAsync(IEnumerable<Guid> userIds, string title, string message, CancellationToken cancellation = default);
    }
}
