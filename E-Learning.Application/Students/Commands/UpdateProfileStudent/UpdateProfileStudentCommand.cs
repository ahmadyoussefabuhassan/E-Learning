using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Students.Commands.UpdateProfileStudent
{
    public sealed record UpdateProfileStudentCommand(
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        IFormFile? ImageUrl,
        string Education
    ) : ICommand<Guid>;
}
