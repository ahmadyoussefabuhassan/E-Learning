using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Notification
{
    public static class NotificationErrors
    {
        public static readonly Error NotFound = new(
            "Notification.not found", "لم يتم العثور على الإشعار المحدد.");
    }
}
