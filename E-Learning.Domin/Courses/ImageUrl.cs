

namespace E_Learning.Domain.Courses
{
    public sealed record ImageUrl
    {
        public string Value { get; init; }
        public ImageUrl(string value)
        {
            if (!Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute))
                throw new ArgumentException("Image URL format is invalid.", nameof(value));
            Value = value;
        }
    }
   
}
