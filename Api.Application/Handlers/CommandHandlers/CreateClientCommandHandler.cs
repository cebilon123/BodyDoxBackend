using System;
using System.Threading.Tasks;
using Api.Application.Handlers.Commands;
using Api.Core.Domain;
using Api.Core.Repositories;

namespace Api.Application.Handlers.CommandHandlers
{
    public class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
    {
        private readonly IClientRepository _clientRepository;

        public CreateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
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
                command.Gender);

            await _clientRepository.InsertClient(client);
        }
    }
}