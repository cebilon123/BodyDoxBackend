using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.Domain;

namespace Api.Core.Repositories
{
    public interface ISessionRepository
    {
        Task CreateSession(Session session);
        Task<Session> GetById(Guid sessionId);
        Task<List<Session>> GetClientSessions(Guid clientId);
        Task<bool> SessionExists(Guid sessionId);
        
        /// <summary>
        /// Updates whole session object.
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="sessionId">Id of the session</param>
        Task UpdateSession(Session session, Guid sessionId);
        
        /// <summary>
        /// This method updates ONLY given annotations in session (to avoid saving whole object on every save)
        /// </summary>
        /// <param name="annotations">Dictionary of annotationId and annotation body</param>
        /// <param name="sessionId">Id of session inside which annotations are going to be saved</param>
        Task UpdateAnnotationsInSession(Dictionary<Guid,Annotation> annotations, Guid sessionId);
    }
}