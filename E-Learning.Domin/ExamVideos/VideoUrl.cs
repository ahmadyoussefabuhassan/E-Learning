using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.ExamVideos
{
    public record VideoUrl
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
