

namespace E_Learning.Domain.ExamVideos
{
    public sealed record ExamVideosVideoUrl
    {
        public string Value { get; init; }
        public ExamVideosVideoUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Video URL cannot be null or empty.", nameof(value));
            Value = value;
        }
    }
}
