using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.Units
{
    public static class UnitNotifications
    {
        public static readonly NotificationTemplate UnitCreated = new(
                 "وحدة دراسية جديدة",
                "تم إضافة وحدة جديدة بعنوان '{0}' في القسم المشترك به. تفقدها الآن!");
    }
}
