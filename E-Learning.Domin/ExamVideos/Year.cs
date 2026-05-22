

namespace E_Learning.Domain.ExamVideos
{
    public sealed record Year
    {
        public int Value { get; init; }
        public Year(int value)
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentException("Year is out of range.", nameof(value));
            Value = value;
        }
    }
}
