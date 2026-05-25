using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Classes
{
    public static class ClassesErrors
    {
        public static readonly Error NotFound = new(
            "Classes.not found" , "لم يتم العثور على الفئة المحددة.");
        public static readonly Error Success = new(
                        "Classes.success" , "تمت العملية بنجاح.");

    }
}
