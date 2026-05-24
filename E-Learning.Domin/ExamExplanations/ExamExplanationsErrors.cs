using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.ExamExplanations
{
    public static class ExamExplanationsErrors
    {
        public static readonly Error NotFound = new(
         "ExamExplanation.NotFound", "شرح الامتحان المطلوب غير موجود في النظام");

    }
}
