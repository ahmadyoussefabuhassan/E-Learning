using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Lessons.Event
{
    public sealed record LessonCreatedDomainEvent(Guid id, string lessonTitle, string url, string titleUrl, Guid unitId) : IDomainEvent
    {
    }
}
