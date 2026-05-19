using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.User
{
    public record Password
    {
        public string Value { get; init; }
        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Password cannot be null or empty.", nameof(value));
            if (value.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long.", nameof(value));
            Value = value;
        }
    }
}
