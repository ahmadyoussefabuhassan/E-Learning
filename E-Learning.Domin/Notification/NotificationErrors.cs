using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Notification
{
    public static class NotificationErrors
    {
        public static readonly Error NotFound = new(
            "Notification.not found" , "لم يتم العثور على الإشعار المحدد.");
    }
}
