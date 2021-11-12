using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.Domain;

namespace Api.Core.Repositories
{
    public interface IClientRepository
    {
        Task InsertClient(Client client);
        Task<Client> GetClient(Guid guid);
        Task<List<Client?>> GetClients(int page, int resultsOnPage, Guid createdBy);
        Task<bool> ClientExist(Guid id);
        Task UpdateClient(Client client, Guid id);
    }
}