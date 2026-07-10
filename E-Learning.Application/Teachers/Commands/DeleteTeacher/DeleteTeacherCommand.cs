using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Teachers.Commands.DeleteTeacher
{
    public sealed record DeleteTeacherCommand(
        Guid TeacherId) : ICommand<bool>;
}
