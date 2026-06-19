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
    }
}
