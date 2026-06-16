namespace E_Learning.Api.Controllers.Lessons
{
    public sealed record AddLessonRequest(
        string Title,
        string TitleUrl,
        IFormFile VidoUrl
    );
}
