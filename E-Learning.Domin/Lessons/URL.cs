
namespace E_Learning.Domain.Lessons
{
    public sealed record URL
    {
        public string Value { get; init; }
        public URL(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("URL cannot be null or empty.", nameof(value));
            Value = value;
        }
    }
}
