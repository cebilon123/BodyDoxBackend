using System.Threading.Tasks;
using Api.Application.Handlers.Queries;
using Api.Application.ResultModels;
using Api.Core.Exceptions.Client;
using Api.Core.Repositories;

namespace Api.Application.Handlers.QueryHandlers
{
    public class GetClientByIdQueryHandler : IQueryHandler<GetClientByIdQuery, Client>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByIdQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> HandleAsync(GetClientByIdQuery query)
        {
            var client = await _clientRepository.GetClient(query.Id) ?? throw new ClientNotFoundException(query.Id);
            return new Client
            {
                Address = client.Address,
                City = client.City,
                Email = client.Email,
                Gender = client.Gender,
                Id = client.Id,
                BirthDate = client.BirthDate,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                UpdatedDate = client.UpdatedDate,
                ZipCode = client.ZipCode
            };
        }
    }
}