

namespace E_Learning.Application.Notifications.Queries.GetMyNotifications
{
    public sealed record NotificationResponse(
       Guid Id,
       string Title,
       string Body,
       DateTime CreatedAt,
       bool IsRead
   );
}
