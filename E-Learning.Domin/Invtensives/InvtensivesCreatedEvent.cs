using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Invtensives
{
    public sealed record InvtensivesCreatedEvent(Guid id, string title, string description, decimal price, Guid courseID) : IDomainEvent;
}