using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Classes
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
    }
}
