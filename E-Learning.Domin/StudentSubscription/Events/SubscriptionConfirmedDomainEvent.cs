
using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.StudentSubscription.Events
{
    public record  SubscriptionConfirmedDomainEvent (Guid Id, Guid StudentId) : IDomainEvent;
}
