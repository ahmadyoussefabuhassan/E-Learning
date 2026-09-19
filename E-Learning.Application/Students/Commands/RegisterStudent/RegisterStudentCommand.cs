using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Students.Commands.RegisterStudent
{
    public sealed record RegisterStudentCommand(
        string FullName,
        string Email,
        string Password,
        string PhoneNumber,
        string Address,

        string Education
    ) : ICommand<Guid>;
}
