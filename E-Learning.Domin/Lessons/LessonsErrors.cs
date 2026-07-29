using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.Lessons
{
    public static class LessonsErrors
    {
        public static readonly Error NotFound = new(
         "Lesson.NotFound", "الدرس المطلوب غير موجود في النظام");
        public static readonly Error FileNotFoundOnServer = new
            ("FileNotFoundOnServer.NotFound", "الفيديو غير موجود في النظام");
        public static readonly Error AccessDenied = new(
            "Lesson.AccessDenied", "لا يمكن تشغيل الفيديو، يرجى الاشتراك أولاً لتتمكن من مشاهدة هذا المحتوى.");
    }
}
