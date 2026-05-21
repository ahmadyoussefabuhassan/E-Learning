using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Classes.Events
{
    public sealed record ClassesCreatedEvent(Guid Id, string Name, string Description, Guid TeachersID, Guid StudentID) : IDomainEvent;
}