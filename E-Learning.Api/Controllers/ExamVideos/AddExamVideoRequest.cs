namespace E_Learning.Api.Controllers.ExamVideos
{
    public sealed record AddExamVideoRequest(IFormFile VidoUrl, int Year , string TitleUrl);
}
