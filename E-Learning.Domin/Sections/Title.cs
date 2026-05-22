
namespace E_Learning.Domain.Sections
{
    public sealed record Title
    {
        public string Value { get; init; }
        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Title cannot be null or empty.", nameof(value));
            if (value.Length > 30)
                throw new ArgumentException("Title cannot exceed 30 characters.", nameof(value));
            Value = value;
        }
    }
}
