using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.ExamExplanations
{
    public static class ExamExplanationsErrors
    {
        public static readonly Error NotFound = new(
         "ExamExplanation.NotFound", "شرح الامتحان المطلوب غير موجود في النظام");

    }
}
