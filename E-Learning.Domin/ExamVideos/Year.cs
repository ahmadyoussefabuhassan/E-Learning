using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.ExamVideos
{
    public record Year
    {
        public int Value { get; init; }
        public Year(int value)
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentException("Year is out of range.", nameof(value));
            Value = value;
        }
    }
}
