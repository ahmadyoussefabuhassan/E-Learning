using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Lessons
{
    public record LessonTitle
    {
        public string Value { get; init; }
        public LessonTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Lesson title cannot be empty.", nameof(value));
            if (value.Length > 30)
                throw new ArgumentException("Lesson title must be at most 30 characters.", nameof(value));
            Value = value;
        }
    }
}
