using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Notifications.Commands.DeleteNotification
{
    public sealed record DeleteNotificationCommand(Guid NotificationId) : ICommand;
}
