

namespace E_Learning.Domain.Shared
{
    public sealed record Description
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
