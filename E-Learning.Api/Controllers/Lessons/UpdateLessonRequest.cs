namespace E_Learning.Api.Controllers.Lessons
{
    public sealed record UpdateLessonRequest(
        string Title,
        string TitleUrl,
        IFormFile VidoUrl
    );
}
