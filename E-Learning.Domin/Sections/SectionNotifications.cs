using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Sections
{
    public static class SectionNotifications
    {
        public static readonly NotificationTemplate Created = new(
         "قسم جديد متاح!",
         "تم إضافة قسم جديد بعنوان '{0}' في الكورس المشترك به.");

        public static readonly NotificationTemplate Updated = new(
            "تحديث في القسم",
            "تم تحديث بيانات القسم: ({0})، تفقد المحتوى الجديد الآن.");
    }
}
