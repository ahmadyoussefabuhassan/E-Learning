using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Invtensives.Events
{
    public sealed record InvtensivesCreatedEvent(Guid id, string title, string description, decimal price, Guid courseID) : IDomainEvent;
}