using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.Sections.Events
{
    public sealed record SectionUpdatedDomainEvent(Guid Id, string Title, decimal Price, Guid CourseId) : IDomainEvent;
}
