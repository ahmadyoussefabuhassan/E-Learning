using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Lessons
{
    public static class LessonsErrors
    {
        public static readonly Error NotFound = new(
         "Lesson.NotFound", "الدرس المطلوب غير موجود في النظام");
        public static readonly Error FileNotFoundOnServer = new
            ("FileNotFoundOnServer.NotFound", "الفيديو غير موجود في النظام");
    }
}
