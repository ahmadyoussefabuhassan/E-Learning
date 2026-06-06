using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Teachers.RegisterTeacher
{
    public sealed record RegisterTeacherCommand(
        string FullName,
        string Email,
        string Password,
        string PhoneNumber,
        string Address,
        string Education,
        string SahmCash

    ) : ICommand<Guid>;
}
