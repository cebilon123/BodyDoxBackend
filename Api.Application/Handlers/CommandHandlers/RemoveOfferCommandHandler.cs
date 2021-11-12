using System.Threading.Tasks;
using Api.Application.Exceptions;
using Api.Application.Handlers.Commands;
using Api.Core.Auth;
using Api.Core.Domain;
using Api.Core.Repositories;

namespace Api.Application.Handlers.CommandHandlers
{
    public class RemoveOfferCommandHandler : ICommandHandler<RemoveOfferCommand>
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IRequestInfoProvider _requestInfoProvider;

        public RemoveOfferCommandHandler(IOfferRepository offerRepository, IRequestInfoProvider requestInfoProvider)
        {
            _offerRepository = offerRepository;
            _requestInfoProvider = requestInfoProvider;
        }
        
        public async Task HandleAsync(RemoveOfferCommand command)
        {
            if (!await _offerRepository.OfferExistsForUser(command.Id, _requestInfoProvider.UserId))
                throw new ResourceNotFoundException(typeof(Offer));

            await _offerRepository.RemoveOffer(command.Id);
        }
    }
}