using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Courses
{
    public record Description
    {
        public string Value { get; init; }
        public Description(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description cannot be null or empty.", nameof(value));
            if (value.Length > 255)
                throw new ArgumentException("Description cannot exceed 255 characters.", nameof(value));
            Value = value;
        }
    }
    
}
