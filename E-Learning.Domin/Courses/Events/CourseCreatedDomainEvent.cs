using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Courses.Events
{
    public sealed record CourseCreatedDomainEvent(Guid Id, string Name, decimal Price, Guid TeacherId, Guid ClassesId) : IDomainEvent;
}
