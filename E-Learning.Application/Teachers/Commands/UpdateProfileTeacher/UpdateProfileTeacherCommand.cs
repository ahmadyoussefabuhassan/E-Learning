using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Teachers.Commands.UpdateProfileTeacher
{
    public sealed record UpdateProfileTeacherCommand(
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        IFormFile ImageUrl,
        string Education,
        string SahmCash
    ) : ICommand<Guid>;

}
