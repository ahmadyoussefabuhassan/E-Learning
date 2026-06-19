using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Notification;


namespace E_Learning.Application.Notifications.Commands.SendBroadcastNotification
{
    namespace E_Learning.Application.Notifications.Commands.SendBroadcastNotification
    {
        public sealed record SendBroadcastNotificationCommand(
            string Title,
            string Message,
            NotificationAudience Audience
        ) : ICommand;
    }
}
