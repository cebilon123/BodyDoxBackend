using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Handlers.Queries;
using Api.Application.ResultModels;
using Api.Core.Auth;
using Api.Core.Repositories;

namespace Api.Application.Handlers.QueryHandlers
{
    public class GetClientsQueryHandler : IQueryHandler<GetClientsQuery, ICollection<Client>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRequestInfoProvider _requestInfoProvider;

        public GetClientsQueryHandler(IClientRepository clientRepository, IRequestInfoProvider requestInfoProvider)
        {
            _clientRepository = clientRepository;
            _requestInfoProvider = requestInfoProvider;
        }

        public async Task<ICollection<Client>> HandleAsync(GetClientsQuery query)
        {
            return (await _clientRepository.GetClients(query.Pagination.PageNumber, query.Pagination.ResultsAmount,
                    _requestInfoProvider.UserId))
                .Select(c => new Client
                {
                    Address = c.Address,
                    City = c.City,
                    Email = c.Email,
                    Gender = c.Gender,
                    Id = c.Id,
                    BirthDate = c.BirthDate,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    UpdatedDate = c.UpdatedDate,
                    ZipCode = c.ZipCode
                }).ToList();
        }
    }
}