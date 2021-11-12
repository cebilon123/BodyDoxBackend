#nullable enable
using System;
using System.Threading.Tasks;
using Api.Core.Domain;

namespace Api.Core.Repositories
{
    public interface IUserSessionsRepository
    {
        Task<UserSession?> GetSession(Guid id);
        Task Insert(UserSession session);
        Task RemoveSession(Guid sessionId);
    }
}