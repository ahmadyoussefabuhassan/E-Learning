using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Roles
{
    public enum NotType
    {
        Admin,
        Teacher,
        Student
    }
    public static class NotTypeExtensions
    {
        public static string ToArabicString(this NotType type)
        {
            return type switch
            {
                NotType.Admin => "أدمن",
                NotType.Teacher => "أستاذ",
                NotType.Student => "طالب",
                _ => "غير معروف"
            };
        }
    }
}
