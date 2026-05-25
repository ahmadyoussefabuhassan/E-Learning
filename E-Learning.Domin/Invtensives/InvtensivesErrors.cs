using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Invtensives
{
    public static class InvtensivesErrors
    {
                public static readonly Error NotFound = new(
                                "Invtensives.not found" , "لم يتم العثور على الدورة المكثفة المحددة.");
    }
}
