using System;
using Api.Core.Repositories;
using Api.Infrastructure.Repositories.Documents;

namespace Api.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoGenericRepository<ClientDocument, Guid> _repository;

        public ClientRepository(IMongoGenericRepository<ClientDocument, Guid> repository)
        {
            _repository = repository;
        }
    }
}