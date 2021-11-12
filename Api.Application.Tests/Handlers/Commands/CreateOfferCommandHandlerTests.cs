using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Handlers;
using Api.Application.Handlers.CommandHandlers;
using Api.Application.Handlers.Commands;
using Api.Core.Auth;
using Api.Core.Const;
using Api.Core.Domain;
using Api.Core.Exceptions;
using Api.Core.Exceptions.Offer;
using Api.Core.Repositories;
using Api.Core.ValueTypes;
using Moq;
using NUnit.Framework;

namespace Api.Application.Tests.Handlers.Commands
{
    public class CreateOfferCommandHandlerTests
    {
        private readonly Guid _userId = Guid.NewGuid();

        private Mock<IOfferRepository> _offerRepositoryMock;
        private Mock<IRequestInfoProvider> _requestInfoProviderMock;
        private Mock<IImageRepository> _imageRepositoryMock;
        private ICommandHandler<CreateOfferCommand> _commandHandler;

        [SetUp]
        public void Setup()
        {
            _offerRepositoryMock = new Mock<IOfferRepository>();
            _requestInfoProviderMock = new Mock<IRequestInfoProvider>();
            _imageRepositoryMock = new Mock<IImageRepository>();

            _requestInfoProviderMock.Setup(c => c.UserId)
                .Returns(_userId);

            _commandHandler = new CreateOfferCommandHandler(_offerRepositoryMock.Object,
                _requestInfoProviderMock.Object, _imageRepositoryMock.Object);
        }

        [TestCase("", "City", "Street", 1, 1, 1)]
        public void HandleAsync_EmptyTitle_ThrowsInvalidTitleException(string title, string city,
            string street, decimal price, double latitude, double longitude)
        {
            var offerCommand = new CreateOfferCommand
            {
                City = city,
                Latitude = latitude,
                Longitude = longitude,
                Price = price,
                Street = street,
                Title = title,
                OfferTypes = new List<OfferType>
                {
                    OfferType.Buy
                }
            };

            var handleAsync = _commandHandler.HandleAsync(offerCommand);
            Assert.ThrowsAsync<InvalidTitleException>(() => handleAsync);
        }

        [TestCase("Title", "", "", 1, 1, 1)]
        public void HandleAsync_EmptyCity_ThrowsInvalidCityException(string title, string city,
            string street, decimal price, double latitude, double longitude)
        {
            var offerCommand = new CreateOfferCommand
            {
                City = city,
                Latitude = latitude,
                Longitude = longitude,
                Price = price,
                Street = street,
                Title = title,
                OfferTypes = new List<OfferType>
                {
                    OfferType.Buy
                }
            };

            var handleAsync = _commandHandler.HandleAsync(offerCommand);
            Assert.ThrowsAsync<InvalidCityException>(() => handleAsync);
        }

        [TestCase("Title", "City", "", 1, 1, 1)]
        public void HandleAsync_EmptyStreet_ThrowsInvalidStreetException(string title, string city,
            string street, decimal price, double latitude, double longitude)
        {
            var offerCommand = new CreateOfferCommand
            {
                City = city,
                Latitude = latitude,
                Longitude = longitude,
                Price = price,
                Street = street,
                Title = title,
                OfferTypes = new List<OfferType>
                {
                    OfferType.Buy
                }
            };

            var handleAsync = _commandHandler.HandleAsync(offerCommand);
            Assert.ThrowsAsync<InvalidStreetException>(() => handleAsync);
        }

        [Test]
        public void HandleAsync_InvalidLatitude_ThrowsInvalidLatitudeException()
        {
            var offerCommand = new CreateOfferCommand
            {
                City = "city",
                Latitude = -91,
                Longitude = 12,
                Price = 1,
                Street = "street",
                Title = "title",
                OfferTypes = new List<OfferType>
                {
                    OfferType.Buy
                }
            };

            var handleAsync = _commandHandler.HandleAsync(offerCommand);
            Assert.ThrowsAsync<InvalidLatitudeException>(() => handleAsync);

            offerCommand.Latitude = 91;
            handleAsync = _commandHandler.HandleAsync(offerCommand);
            Assert.ThrowsAsync<InvalidLatitudeException>(() => handleAsync);
        }

        [Test]
        public void HandleAsync_InvalidLongitude_ThrowsInvalidLongitudeException()
        {
            var offerCommand = new CreateOfferCommand
            {
                City = "city",
                Latitude = 23,
                Longitude = 181,
                Price = 1,
                Street = "street",
                Title = "title",
                OfferTypes = new List<OfferType>
                {
                    OfferType.Buy
                }
            };

            var handleAsync = _commandHandler.HandleAsync(offerCommand);
            Assert.ThrowsAsync<InvalidLongitudeException>(() => handleAsync);

            offerCommand.Longitude = -181;
            handleAsync = _commandHandler.HandleAsync(offerCommand);
            Assert.ThrowsAsync<InvalidLongitudeException>(() => handleAsync);
        }

        [Test]
        public async Task HandleAsync_ValidOffer_InsertsOfferToRepository()
        {
            var addedToRepository = false;
            var offerCommand = new CreateOfferCommand
            {
                City = "city",
                Latitude = 23,
                Longitude = 21,
                Price = new decimal(143.23),
                Street = "street",
                Title = "title",
                OfferTypes = new List<OfferType>
                {
                    OfferType.Buy
                }
            };

            _offerRepositoryMock.Setup(m => m.Insert(It.IsAny<Offer>()))
                .Callback(() => addedToRepository = true);

            await _commandHandler.HandleAsync(offerCommand);

            Assert.That(addedToRepository);
        }
    }
}