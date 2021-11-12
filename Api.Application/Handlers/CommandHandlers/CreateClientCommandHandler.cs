using System;
using System.Threading.Tasks;
using Api.Application.Handlers.Commands;
using Api.Core.Auth;
using Api.Core.Domain;
using Api.Core.Repositories;

namespace Api.Application.Handlers.CommandHandlers
{
    public class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRequestInfoProvider _requestInfoProvider;

        public CreateClientCommandHandler(IClientRepository clientRepository, IRequestInfoProvider requestInfoProvider)
        {
            _clientRepository = clientRepository;
            _requestInfoProvider = requestInfoProvider;
        }

        public async Task HandleAsync(CreateClientCommand command)
        {
            var client = new Client(
                Guid.NewGuid(),
                command.FirstName,
                command.LastName,
                command.Email,
                command.BirthDate,
                command.Address,
                command.ZipCode,
                command.City,
                command.PhoneNumber,
                DateTime.Now.ToUniversalTime(),
                _requestInfoProvider.UserId,
                command.Gender);

            await _clientRepository.InsertClient(client);
        }
    }
}