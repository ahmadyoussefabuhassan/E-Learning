using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.User.Events
{
    public sealed record UserCreatedDomainEvent(Guid UserId,
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        string ImageUrl
    ) : IDomainEvent;
}
