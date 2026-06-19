using E_Learning.Domain.Notification;

namespace E_Learning.Api.Controllers.Notifications
{
    public sealed record SendBroadcastNotificationRequest(
        string Title,
        string Message,
        NotificationAudience Audience
    );
}
