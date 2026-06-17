using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Application.ExamVideos.Queries.GetAllExamVideosByExam
{
    public sealed record ExamVidoeResponse(Guid Id , string VidoeUrl , int Yaer);
}
