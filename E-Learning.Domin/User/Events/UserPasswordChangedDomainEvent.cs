using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.User.Events
{
    public record UserPasswordChangedDomainEvent(Guid UserId, string Email) : IDomainEvent;
}
