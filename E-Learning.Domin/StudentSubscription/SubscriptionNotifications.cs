

using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.StudentSubscription
{
    public static class SubscriptionNotifications
    {
        public static readonly NotificationTemplate RequestReceived = new(
                        "طلب اشتراك جديد",
                 $"قام الطالب {0} بإرسال طلب اشتراك جديد. يرجى مراجعة وصل الدفع."
        );
        public static readonly NotificationTemplate Accepted = new(
                             "مبروك! تم تفعيل اشتراكك",
                    "عزيزي الطالب، تم قبول طلب اشتراكك بنجاح. يمكنك الآن البدء بالدراسة."
        );
        public static readonly NotificationTemplate Rejected = new(
                         "تحديث بخصوص طلب الاشتراك",
                    "نعتذر منك، تم رفض طلب اشتراكك. يرجى التأكد من صحة وصل الدفع والمحاولة مرة أخرى."
        );
    }
}
