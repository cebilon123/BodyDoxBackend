using System;
using System.Threading.Tasks;
using Api.Core.Domain;

namespace Api.Core.Repositories
{
    public interface IClientRepository
    {
        Task InsertClient(Client client);
        Task<Client> GetClient(Guid guid);
        Task<bool> ClientExist(Guid id);
        Task UpdateClient(Client client, Guid id);
    }
}