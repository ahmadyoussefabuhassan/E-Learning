using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Courses
{
    public record IsActive
    {
        
        public bool Value { get; init; }
        public IsActive(bool value)
        {
            Value = value;
        }
    }
}
