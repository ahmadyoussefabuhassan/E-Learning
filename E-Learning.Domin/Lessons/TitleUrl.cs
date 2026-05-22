

namespace E_Learning.Domain.Lessons
{
    public sealed record TitleUrl
    {
        public string Value { get; init; }
        public TitleUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Title URL cannot be null or empty.", nameof(value));
            if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
                throw new ArgumentException("Title URL format is invalid.", nameof(value));
            Value = value;
        }
    }
}
