namespace E_Learning.Domain.Abstractions.JWT
{
    public sealed class JwtSettings
    {
        public string Key { get; init; } = null!;
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public double DurationInDays { get; set; }
    }
}
