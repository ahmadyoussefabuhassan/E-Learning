using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.User.Events
{
    public sealed record UserCreatedDomainEvent(Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string PhoneNumber,
        string Address,
        string ImageUrl
    ) : IDomainEvent;
}
