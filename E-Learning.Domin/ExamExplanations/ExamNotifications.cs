using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.ExamExplanations
{
    public static class ExamNotifications
    {
        public static readonly NotificationTemplate ExamExplanationCreated = new(
      "أسئلة دورات جديدة! 🔥",
      "تمت إضافة شرح جديد لأسئلة الدورات بعنوان '{0}'. اطلع عليه الآن وطوّر مستواك!");
    }
}
