using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Lessons.Queries.GetLessonStream
{
    public sealed record GetLessonStreamQuery(Guid LessonId) : IQuery<FileStream>;
}
