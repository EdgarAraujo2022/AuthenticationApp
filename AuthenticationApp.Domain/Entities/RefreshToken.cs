using System;

namespace AuthenticationApp.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }
        public Guid ClientId { get; private set; }
        public string Token { get; private set; } = string.Empty;
        public DateTime ExpiresAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public Guid UserId { get; private set; }

        private RefreshToken() { }

        public RefreshToken(Guid userId, Guid clientId, string token, DateTime expiresAt )
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
        }

        public void Revoke()
        {
            RevokedAt = DateTime.UtcNow;
        }

        public bool IsActive => RevokedAt == null && DateTime.UtcNow <= ExpiresAt;
    }
}
