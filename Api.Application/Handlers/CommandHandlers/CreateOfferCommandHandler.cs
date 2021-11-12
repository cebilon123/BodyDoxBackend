using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Handlers.Commands;
using Api.Core.Auth;
using Api.Core.Domain;
using Api.Core.Repositories;
using Api.Core.ValueTypes;

namespace Api.Application.Handlers.CommandHandlers
{
    public class CreateOfferCommandHandler : ICommandHandler<CreateOfferCommand>
    {
        private const string ImageExtension = ".jpg";

        private readonly IOfferRepository _offerRepository;
        private readonly IRequestInfoProvider _requestInfoProvider;
        private readonly IImageRepository _imageRepository;

        public CreateOfferCommandHandler(IOfferRepository offerRepository, IRequestInfoProvider requestInfoProvider,
            IImageRepository imageRepository)
        {
            _offerRepository = offerRepository;
            _requestInfoProvider = requestInfoProvider;
            _imageRepository = imageRepository;
        }

        public async Task HandleAsync(CreateOfferCommand command)
        {
            var offerId = Guid.NewGuid();
            var images = new List<Image>();

            var imagesDictionary = new Dictionary<string, byte[]>();

            command.ImagesReadonly.ToList().ForEach(image =>
            {
                var imageGuid = Guid.NewGuid();

                imagesDictionary.Add(imageGuid + ImageExtension, image.ImageBytes);
            });

            var result = await _imageRepository.InsertImages(offerId.ToString(), imagesDictionary);

            imagesDictionary.ToList().ForEach(img =>
            {
                var url = result.First(r => r.Contains(img.Key));
                images.Add(new Image(url, img.Key));
            });

            var offer = new Offer(offerId, images, command.Title, command.City, command.Street, command.Price,
                command.Latitude, command.Longitude, _requestInfoProvider.UserId, command.OfferTypes);

            await _offerRepository.Insert(offer);
        }
    }
}