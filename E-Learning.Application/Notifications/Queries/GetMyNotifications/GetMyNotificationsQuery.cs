using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.Notifications.Queries.GetMyNotifications
{
    public sealed record GetMyNotificationsQuery() : IQuery<IEnumerable<NotificationResponse>>;
}
