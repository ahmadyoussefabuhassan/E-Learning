namespace E_Learning.Api.Controllers.ExamVideos
{
    public sealed record UpdateExamVideoRequest(IFormFile VidoUrl, int Year, string TitleUrl);
}
