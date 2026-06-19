using E_Learning.Domain.Roles;

namespace E_Learning.Domain.StudentSubscription
{
    public sealed record TargetType(string Value);
    public enum TargetTypes
    {
        Course = 1,
        ExamExplanation = 2,
        Invtensive = 3,
        Section = 4,
    }
    public static class TargetTypesExtensions
    {
        public static string ToArabicString(this TargetTypes type)
        {
            return type switch
            {
                TargetTypes.Course => "كورس",
                TargetTypes.Section => "قسم",
                TargetTypes.Invtensive => "مكثفة",
                TargetTypes.ExamExplanation => "أسئلة دورات",
                _ => "غير معروف"
            };
        }
    }
}
