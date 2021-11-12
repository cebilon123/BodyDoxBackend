using System;
using Api.Core.Exceptions.UserSession;

namespace Api.Core.Domain
{
    public class UserSession
    {
        /// <summary>
        /// Session id
        /// </summary>
        public Guid Id { get; }

        public Guid UserId { get; }
        public string Token { get; }
        public string RefreshToken { get; }
        public DateTime CreatedAt { get; }

        public UserSession(Guid id, Guid userId, DateTime createdAt, string token, string refreshToken)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;

            if (userId == Guid.Empty)
                throw new InvalidUserId(userId.ToString());

            UserId = userId;
            CreatedAt = createdAt;
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}