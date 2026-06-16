
namespace E_Learning.Application.Lessons.Queries.GetLessonById
{
    public sealed record LessonResponse(Guid Id, string title, string titleurl, string VideoStreamingUrl);
}
