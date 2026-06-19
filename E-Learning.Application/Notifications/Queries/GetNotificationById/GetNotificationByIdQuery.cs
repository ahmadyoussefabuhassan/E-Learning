using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.Notifications.Queries.GetNotificationById
{
    public sealed record GetNotificationByIdQuery(Guid Id) : IQuery<NotificationResponse>;
}
