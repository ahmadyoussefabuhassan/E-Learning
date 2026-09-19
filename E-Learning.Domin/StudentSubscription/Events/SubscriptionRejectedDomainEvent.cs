using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.StudentSubscription.Events
{
    public sealed record SubscriptionRejectedDomainEvent(Guid SubscriptionId, Guid StudentId) : IDomainEvent;
}
