using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.Courses
{
    public static class CourseErrors
    {
        public static readonly Error NotFound = new(
         "Course.NotFound", "المادة المطلوب غير موجودة في النظام");
        public static readonly Error InvalidCourseName = new(
         "Course.InvalidCourseName", "اسم المقرر غير صالح");
    }
}
