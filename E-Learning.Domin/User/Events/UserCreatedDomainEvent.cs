using E_Learning.Domin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domin.User.Events
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
