using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Students.Events
{
    public sealed record StudentCreatedDomainEvent(Guid Id, string Subject) : IDomainEvent;
}
