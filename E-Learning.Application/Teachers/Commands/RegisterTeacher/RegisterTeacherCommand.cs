using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Teachers.Commands.RegisterTeacher
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
