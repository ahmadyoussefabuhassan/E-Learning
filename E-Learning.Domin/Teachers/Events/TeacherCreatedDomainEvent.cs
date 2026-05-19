using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Teachers.Events
{
    public sealed record TeacherCreatedDomainEvent(Guid Id, string UrlShamCash, string Subject) : IDomainEvent;
}
