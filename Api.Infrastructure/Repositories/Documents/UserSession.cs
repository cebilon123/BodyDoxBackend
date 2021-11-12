using System;

namespace Api.Infrastructure.Repositories.Documents
{
    public class UserSessionDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}