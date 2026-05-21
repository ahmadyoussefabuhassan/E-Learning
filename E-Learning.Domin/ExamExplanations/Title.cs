using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.ExamExplanations
{
    public record Title
    {
        public string Value { get; init; }
        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Title cannot be null or empty.", nameof(value));
            if (value.Length > 30)
                throw new ArgumentException("Title must be at most 30 characters.", nameof(value));
            Value = value;
        }
    }
}
