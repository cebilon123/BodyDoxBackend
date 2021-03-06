using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Domain;
using Api.Core.Repositories;
using Api.Infrastructure.Repositories.Documents;
using MongoDB.Driver;

namespace Api.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoGenericRepository<ClientDocument, Guid> _repository;

        public ClientRepository(IMongoGenericRepository<ClientDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task InsertClient(Client client)
            => await _repository.AddAsync(client.AsDocument());

        public async Task<Client?> GetClient(Guid guid)
            => (await _repository.GetAsync(c => c.Id == guid)).AsEntity();

        public async Task<List<Client?>> GetClients(int page, int resultsOnPage, Guid createdById)
            => (await _repository.Collection.Find(c => c.CreatedBy == createdById).Skip(page).Limit(resultsOnPage)
                .ToListAsync()).Select(c => c.AsEntity()).ToList();

        public async Task<bool> ClientExist(Guid id)
            => await _repository.ExistsAsync(c => c.Id == id);

        public async Task UpdateClient(Client client, Guid id)
            => await _repository.UpdateAsync(client.AsDocument(), c => c.Id == id);
    }
}