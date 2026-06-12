using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Courses.Commands.DeleteCourse
{
    public sealed record DeleteCourseCommand(Guid Id) : ICommand<bool>;
}
