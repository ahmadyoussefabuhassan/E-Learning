

namespace E_Learning.Domain.Units
{
    public sealed record UnitTitle
    {
        public string Value { get; init; }
        public UnitTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Title cannot be null or empty.", nameof(value));
            if (value.Length > 30)
                throw new ArgumentException("Title must be at most 30 characters.", nameof(value));
            Value = value;
        }
    }
}
