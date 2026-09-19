using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.ExamExplanations.Events
{
    public sealed record ExamExplanationCreatedEvent(Guid Id, string Title, string Description, decimal Price, Guid CourseId) : IDomainEvent;

}
