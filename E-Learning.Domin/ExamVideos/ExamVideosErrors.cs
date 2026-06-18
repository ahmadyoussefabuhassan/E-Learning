using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.ExamVideos
{
    public static class ExamVideosErrors
    {
        public static readonly Error NotFound = new(
         "ExamVideo.NotFound", "فيديو الامتحان المطلوب غير موجود في النظام");
        public static readonly Error FileNotFoundOnServer = new
             ("FileNotFoundOnServer.NotFound", "الفيديو غير موجود في النظام");
    }
}
