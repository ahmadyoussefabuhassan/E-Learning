

namespace E_Learning.Domain.ExamVideos
{
    public sealed record VideoUrl
    {
        public string Value { get; init; }
        public VideoUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Video URL cannot be null or empty.", nameof(value));
            Value = value;
        }
    }
}
