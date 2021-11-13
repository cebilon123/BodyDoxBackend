using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Domain;
using Api.Core.Repositories;
using Api.Infrastructure.Repositories.Documents;
using Annotation = Api.Core.Domain.Annotation;

namespace Api.Infrastructure.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IMongoGenericRepository<SessionDocument, Guid> _repository;

        public SessionRepository(IMongoGenericRepository<SessionDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task CreateSession(Session session)
            => await _repository.AddAsync(session.AsDocument());

        public async Task<Session?> GetById(Guid sessionId)
            => (await _repository.GetAsync(sessionId)).AsEntity();

        public async Task<List<Session?>> GetClientSessions(Guid clientId)
            => (await _repository
                    .FindAsync(s => s.ClientId == clientId)).Select(s => s.AsEntity())
                .ToList();

        public async Task<bool> SessionExists(Guid sessionId)
            => await _repository.ExistsAsync(s => s.Id == sessionId);

        public async Task UpdateSession(Session session, Guid sessionId)
            => await _repository.UpdateAsync(session.AsDocument(), s => s.Id == sessionId);

        public Task UpdateAnnotationsInSession(Dictionary<Guid, Annotation> annotations, Guid sessionId)
        {
            throw new NotImplementedException();
        }
    }
}