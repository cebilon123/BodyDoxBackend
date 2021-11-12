using System.Threading.Tasks;
using Api.Core.Domain;

namespace Api.Core.Repositories
{
    public interface IClientRepository
    {
        Task InsertClient(Client client);
    }
}