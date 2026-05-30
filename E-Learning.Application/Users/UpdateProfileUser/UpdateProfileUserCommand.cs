using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;


namespace E_Learning.Application.Users.UpdateProfileUser
{
    public sealed record UpdateProfileUserCommand(
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        IFormFile ImageUrl
    ) : ICommand<Guid>;
}
