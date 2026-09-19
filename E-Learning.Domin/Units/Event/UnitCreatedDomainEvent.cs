using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Units.Event
{
    public sealed record UnitCreatedDomainEvent(Guid UnitId, string Title, string Description, Guid SectionId) : IDomainEvent
    {
    }
}
