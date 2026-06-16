using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.Lessons.Queries.GetLessonById
{
    public sealed record GetLessonByIdQuery(Guid lessonId) : IQuery<LessonResponse>;
}
