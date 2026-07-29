using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.InvtensivesVideos
{
    public static class InvtensivesVideosErrors
    {
                public static readonly Error NotFound = new(
                  "InvtensivesVideos.not found" , "لم يتم العثور على فيديو الدورة المكثفة المحدد.");
        public static readonly Error FileNotFoundOnServer = new
            ("FileNotFoundOnServer.NotFound", "الفيديو غير موجود في النظام");
        public static readonly Error AccessDenied = new(
            "InvtensivesVideos.AccessDenied", "لا يمكن تشغيل الفيديو، يرجى الاشتراك أولاً لتتمكن من مشاهدة هذا المحتوى.");
    }
}
