using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Lessons.Commands.DeleteLesson
{
    public sealed record DeleteLessonCommand(Guid Id): ICommand<bool>;
}
