using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Courses
{
    public static class CourseErrors
    {
        public static readonly Error NotFound = new(
         "Course.NotFound", "المقرر المطلوب غير موجودة في النظام");
        public static readonly Error InvalidCourseName = new(
         "Course.InvalidCourseName", "اسم المقرر غير صالح");
    }
}
