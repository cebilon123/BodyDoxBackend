using System;
using System.Threading.Tasks;
using Api.Core.Domain;
using Api.Core.Repositories;
using Api.Infrastructure.Repositories.Documents;

namespace Api.Infrastructure.Repositories
{
    public class UserSessionRepository : IUserSessionsRepository
    {
        private readonly IMongoGenericRepository<UserSessionDocument, Guid> _repository;

        public UserSessionRepository(IMongoGenericRepository<UserSessionDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<UserSession?> GetSession(Guid id)
        {
            return (await _repository.GetAsync(u => u.Id == id)).AsEntity();
        }

        public async Task Insert(UserSession session)
        {
            await _repository.AddAsync(session.AsDocument());
        }

        public async Task RemoveSession(Guid sessionId)
        {
            await _repository.DeleteAsync(u => u.Id == sessionId);
        }
    }
}