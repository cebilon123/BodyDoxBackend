using System;
using System.Threading.Tasks;
using Api.Application.Handlers.Commands;
using Api.Core.Domain;
using Api.Core.Exceptions.Client;
using Api.Core.Repositories;

namespace Api.Application.Handlers.CommandHandlers
{
    public class UpdateClientCommandHandler : ICommandHandler<UpdateClientCommand>
    {
        private readonly IClientRepository _clientRepository;

        public UpdateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task HandleAsync(UpdateClientCommand command)
        {
            if (!await _clientRepository.ClientExist(command.Id))
                throw new ClientNotFoundException(command.Id);

            var client = await _clientRepository.GetClient(command.Id);

            var updateClient = new Client(
                client.Id, 
                command.FirstName, 
                command.LastName, 
                command.Email,
                command.BirthDate, 
                command.Address, 
                command.ZipCode, 
                command.City, 
                command.PhoneNumber,
                client.CreatedDate, 
                command.Gender, 
                null, 
                DateTime.Now.ToUniversalTime());

            await _clientRepository.UpdateClient(updateClient, client.Id);
        }
    }
}