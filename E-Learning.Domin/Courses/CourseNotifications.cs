using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.Courses
{
    public static class CourseNotifications
    {
        public static readonly NotificationTemplate CourseCreated = new(
                "كورس جديد متاح!",
                $"تم إضافة كورس جديد بعنوان '{0}' بواسطة الأستاذ '{1}'"
        );
        public static readonly NotificationTemplate CourseUpdated = new(
          "تحديث في الكورس",
          $"قام الأستاذ بتحديث بيانات كورس '{0}'."
        );
        public static readonly NotificationTemplate Updated = new(
          "تحديث في محتوى الكورس",
          $"عزيزي الطالب، تم تحديث محتوى الكورس: ({0}). تفقد التغييرات الجديدة الآن!"
        );
    }
}
