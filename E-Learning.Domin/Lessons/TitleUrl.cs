

namespace E_Learning.Domain.Lessons
{
    public sealed record TitleUrl
    {
        public string Value { get; init; }
        public TitleUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Title URL cannot be null or empty.", nameof(value));
            Value = value;
        }
    }
}
