using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Notification.Events
{
    public sealed record NotificationCreatedEvent(Guid Id, Guid UserId, string Message, string Title, string UrlRedirect, bool IsRead, DateTime CreatedAt) : IDomainEvent;
}