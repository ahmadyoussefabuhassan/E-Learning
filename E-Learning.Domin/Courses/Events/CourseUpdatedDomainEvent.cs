using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Courses.Events
{
    public sealed record CourseUpdatedDomainEvent(Guid Id, string Name, decimal Price, Guid ClassesId) : IDomainEvent;
}
