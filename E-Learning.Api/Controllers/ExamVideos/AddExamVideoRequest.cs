namespace E_Learning.Api.Controllers.ExamVideos
{
    public sealed record AddExamVideoRequest(IFormFile VidoeUrl, int Year , string TitleUrl);
}
