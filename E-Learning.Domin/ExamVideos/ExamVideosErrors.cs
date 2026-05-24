using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.ExamVideos
{
    public static class ExamVideosErrors
    {
        public static readonly Error NotFound = new(
         "ExamVideo.NotFound", "فيديو الامتحان المطلوب غير موجود في النظام");
    }
}
