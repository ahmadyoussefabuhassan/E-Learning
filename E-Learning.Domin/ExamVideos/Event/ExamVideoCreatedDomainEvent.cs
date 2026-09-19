using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.ExamVideos.Event
{
    public sealed record ExamVideoCreatedDomainEvent(Guid Id, string VideoUrl, int Year, Guid ExamExplanationId) : IDomainEvent;
}
