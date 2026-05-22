

namespace E_Learning.Domain.Lessons
{
    public sealed record LessonTitle
    {
        public string Value { get; init; }
        public LessonTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Lesson title cannot be empty.", nameof(value));
            Value = value;
        }
    }
}
