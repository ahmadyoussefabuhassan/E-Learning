using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Students.LogInStudent
{
    public sealed record LogInStudentCommand(
        string Email,
        string Password
    ) : ICommand<AuthenticationResponse>;
}
