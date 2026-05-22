

namespace E_Learning.Domain.Shared
{
    public sealed record Price
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

