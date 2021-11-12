using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Exceptions;
using Api.Application.Handlers;
using Api.Application.Handlers.CommandHandlers;
using Api.Application.Handlers.Commands;
using Api.Core.Auth;
using Api.Core.Const;
using Api.Core.Domain;
using Api.Core.Exceptions.Offer;
using Api.Core.Repositories;
using Api.Core.ValueTypes;
using Moq;
using NUnit.Framework;

namespace Api.Application.Tests.Handlers.Commands
{
    public class UpdateOfferCommandHandlerTests
    {
        private readonly Guid _userId = Guid.NewGuid();

        private Mock<IOfferRepository> _offerRepositoryMock;
        private Mock<IRequestInfoProvider> _requestInfoProviderMock;
        private ICommandHandler<UpdateOfferCommand> _commandHandler;

        [SetUp]
        public void SetUp()
        {
            _offerRepositoryMock = new Mock<IOfferRepository>();
            _requestInfoProviderMock = new Mock<IRequestInfoProvider>();

            _requestInfoProviderMock.Setup(c => c.UserId)
                .Returns(_userId);

            _commandHandler =
                new UpdateOfferCommandHandler(_offerRepositoryMock.Object, _requestInfoProviderMock.Object);
        }

        [Test]
        public async Task HandleAsync_ThereIsNoOfferWithOfferId_ThrowsResourceNotFoundException()
        {
            var resourceId = Guid.NewGuid();
            var command = new UpdateOfferCommand(resourceId, new UpdateOfferCommand.UpdateOfferData());

            Assert.ThrowsAsync<ResourceNotFoundException>(() => _commandHandler.HandleAsync(command));
        }

        [TestCase("", "City", "street", 120, typeof(InvalidTitleException))]
        [TestCase("Title", "", "street", 120, typeof(InvalidCityException))]
        [TestCase("Title", "City", "", 120, typeof(InvalidStreetException))]
        public async Task HandleAsync_InvalidFieldsInUpdateModel_ThrowsVariousExceptions(string title, string city,
            string street, decimal price, Type exceptionType)
        {
            var offerId = Guid.NewGuid();
            var command = new UpdateOfferCommand(offerId, new UpdateOfferCommand.UpdateOfferData
            {
                City = city,
                Price = price,
                Street = street,
                Title = title,
                OfferTypes = new List<OfferType>()
            });

            _offerRepositoryMock.Setup(c =>
                    c.OfferExistsForUser(It.Is<Guid>(a => a == offerId), It.Is<Guid>(a => a == _userId)))
                .ReturnsAsync(true);

            _offerRepositoryMock.Setup(c => c.GetOffer(It.Is<Guid>(a => a == offerId)))
                .ReturnsAsync(new Offer(
                    offerId,
                    new List<Image>(),
                    "Title",
                    "City",
                    "Street 22",
                    123,
                    23.42,
                    15.32,
                    _userId,
                    new List<OfferType>()
                ));

            Assert.ThrowsAsync(exceptionType, () => _commandHandler.HandleAsync(command));
        }

        [Test]
        public async Task HandleAsync_ValidOfferModel_OfferUpdated()
        {
            var offerId = Guid.NewGuid();
            var command = new UpdateOfferCommand(offerId, new UpdateOfferCommand.UpdateOfferData
            {
                City = "city",
                Price = 123,
                Street = "street",
                Title = "title",
                OfferTypes = new List<OfferType>()
            });

            _offerRepositoryMock.Setup(c =>
                    c.OfferExistsForUser(It.Is<Guid>(a => a == offerId), It.Is<Guid>(a => a == _userId)))
                .ReturnsAsync(true);

            _offerRepositoryMock.Setup(c => c.GetOffer(It.Is<Guid>(a => a == offerId)))
                .ReturnsAsync(new Offer(
                    offerId,
                    new List<Image>(),
                    "Title",
                    "City",
                    "Street 22",
                    123,
                    23.42,
                    15.32,
                    _userId,
                    new List<OfferType>()
                ));

            var updateCalled = false;
            _offerRepositoryMock.Setup(c => c.Update(It.IsAny<Offer>()))
                .Callback(() => updateCalled = true);

            await _commandHandler.HandleAsync(command);
            
            Assert.IsTrue(updateCalled);
        }
    }
}