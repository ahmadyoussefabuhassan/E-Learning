using System;
namespace E_Learning.Domain.Courses
{
    public record Name
    {
      public string Value { get; init; }
        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Course name cannot be null or empty.", nameof(value));
            if (value.Length < 3)
                throw new ArgumentException("Course name must be at least 3 characters long.", nameof(value));
            Value = value;
        }
    }
}
