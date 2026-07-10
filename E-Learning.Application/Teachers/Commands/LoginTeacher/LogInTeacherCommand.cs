using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Teachers.Commands.LoginTeacher
{
    public sealed record LogInTeacherCommand(
        string Email,
        string Password
    ) : ICommand<AuthenticationResponse>;
}
