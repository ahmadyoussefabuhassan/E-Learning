using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.StudentSubscription
{
    public static class StudentSubscriptionErrors
    {
        public static readonly Error NotFound = new(
            "StudentSubscription.not found" , "لم يتم العثور على اشتراك الطالب المحدد.");
        public static readonly Error Duplicate =
            new Error("Subscription.Duplicate", "لقد قمت بإرسال طلب اشتراك لهذا الكورس مسبقاً.");
        public static readonly Error AlreadyActivated =
            new Error("Subscription.AlreadyActivated", "تم تفعيل الاشتراك مسبقاً.");
        public static readonly Error ActivatorNotFound = new(
            "Subscription.ActivatorNotFound", "لم يتم العثور على منشط الاشتراك.");
    }
}
