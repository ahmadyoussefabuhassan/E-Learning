using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Teachers.Commands.AddTeacher
{
    public sealed record  AddTeacherCommand(
        string FullName,
        string Email,
        string Password,
        string PhoneNumber,
        string Address,
        string Education,
        string SahmCash
    ) : ICommand<Guid>;

}
