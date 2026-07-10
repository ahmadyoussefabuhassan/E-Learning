using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Teachers
{
    public static class TeacherErrors
    {
        public static readonly Error NotFound = new(
                                     "Teacher.NotFound", "الأستاذ المطلوب غير موجود");
        public static readonly Error HasRelatedData = 
            new Error("Teacher.HasRelatedData", "لا يمكن حذف الأستاذ لوجود كورسات مرتبطة به.");
    }
}
