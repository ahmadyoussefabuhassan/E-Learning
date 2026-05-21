using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Notification
{
    public sealed record NotificationReadEvent(Guid Id) : IDomainEvent;
}