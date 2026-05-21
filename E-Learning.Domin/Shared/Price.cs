using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Shared
{
    public record Price
    {
        public decimal Value { get; init; }
        public Price(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(value));
            Value = value;
        }
    }
}

