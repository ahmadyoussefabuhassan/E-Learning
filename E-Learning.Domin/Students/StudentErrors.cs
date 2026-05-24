
using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Students
{
    public static class StudentErrors
    {
        public static readonly Error NotFound = new(
            "Student.NotFound", "بيانات الطالب غير موجودة في النظام");
    }
}
