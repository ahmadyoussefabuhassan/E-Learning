using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.RefreshTokens
{
    public sealed class RefreshToken : Entity
    {
        private RefreshToken() : base(Guid.Empty)
        {
        }
        private RefreshToken(Guid Id, string token, string jWTId, DateTime createdAt, DateTime expires, Guid UserId) : base(Id)
        {
            Token = token;
            JWTId = jWTId;
            CreatedAt = createdAt;
            Expires = expires;
            IsUsed = false;
            IsRevoked = false;
            this.UserId = UserId;
        }
        public string Token { get; private set; }
        public string JWTId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime Expires { get; private set; }
        public bool IsUsed { get; private set; }
        public bool IsRevoked { get; private set; }
        public Guid UserId { get; private set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public static RefreshToken Create(string token, string jWTId, DateTime createdAt, DateTime expires, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Token cannot be empty");
            if (string.IsNullOrWhiteSpace(jWTId)) throw new ArgumentException("JWTId cannot be empty");
            if (expires <= DateTime.UtcNow) throw new ArgumentException("Expiry date must be in the future");
            return new RefreshToken(Guid.NewGuid(), token, jWTId, createdAt, expires, userId);
        }
    }
}
