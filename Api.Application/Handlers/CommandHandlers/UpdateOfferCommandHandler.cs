using System.Threading.Tasks;
using Api.Application.Exceptions;
using Api.Application.Handlers.Commands;
using Api.Core.Auth;
using Api.Core.Domain;
using Api.Core.Repositories;

namespace Api.Application.Handlers.CommandHandlers
{
    public class UpdateOfferCommandHandler : ICommandHandler<UpdateOfferCommand>
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IRequestInfoProvider _requestInfoProvider;

        public UpdateOfferCommandHandler(IOfferRepository offerRepository, IRequestInfoProvider requestInfoProvider)
        {
            _offerRepository = offerRepository;
            _requestInfoProvider = requestInfoProvider;
        }
        
        public async Task HandleAsync(UpdateOfferCommand command)
        {
            if (!await _offerRepository.OfferExistsForUser(command.Id, _requestInfoProvider.UserId))
                throw new ResourceNotFoundException(typeof(Offer));

            var offer = await _offerRepository.GetOffer(command.Id);

            if (offer is null)
                throw new ResourceNotFoundException(typeof(Offer));
            
            var updatedOffer = new Offer(
                offer.Id,
                offer.Images,
                command.UpdateOfferDataData.Title,
                command.UpdateOfferDataData.City,
                command.UpdateOfferDataData.Street,
                command.UpdateOfferDataData.Price,
                offer.Latitude,
                offer.Longitude,
                offer.AuthorId,
                command.UpdateOfferDataData.OfferTypes);

            await _offerRepository.Update(updatedOffer);
        }
    }
}