using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Handlers;
using Api.Application.Handlers.Queries;
using Api.Application.Handlers.QueryHandlers;
using Api.Core.Const;
using Api.Core.Domain;
using Api.Core.Repositories;
using Api.Infrastructure.Repositories.Documents;
using MongoDB.Driver.GeoJsonObjectModel;
using Moq;
using NUnit.Framework;
using Image = Api.Core.ValueTypes.Image;

namespace Api.Application.Tests.Handlers.Queries
{
    public class GetOfferByIdQueryHandlerTests
    {
        private Mock<IOfferRepository> _offerRepositoryMock;
        private IQueryHandler<GetOfferByIdQuery, GetOfferByIdQueryResult> _queryHandler;

        [SetUp]
        public void SetUp()
        {
            _offerRepositoryMock = new Mock<IOfferRepository>();
            _queryHandler = new GetOfferByIdQueryHandler(_offerRepositoryMock.Object);
        }

        [Test]
        public async Task HandleAsync_OfferWithIdExists_ReturnOffer()
        {
            var offerId = Guid.NewGuid();

            _offerRepositoryMock.Setup(o => o.GetOffer(It.Is<Guid>(id => id == offerId)))
                .ReturnsAsync(ValidOfferWithAllInformation(offerId));

            var result = await _queryHandler.HandleAsync(new GetOfferByIdQuery(offerId));

            Assert.That(result != null);
            Assert.That(result.GetType() == typeof(GetOfferByIdQueryResult));
            Assert.That(result.Offer != null);
            Assert.That(result.Offer.GetType() == typeof(ResultModels.Offer));
        }

        [Test]
        public async Task HandleAsync_OfferWithIdDontExist_ReturnEmptyResult()
        {
            var offerId = Guid.NewGuid();

            _offerRepositoryMock.Setup(o => o.GetOffer(It.Is<Guid>(id => id == offerId)))
                .ReturnsAsync((Offer) null);

            var result = await _queryHandler.HandleAsync(new GetOfferByIdQuery(offerId));

            Assert.That(result != null);
            Assert.That(result.GetType() == typeof(GetOfferByIdQueryResult));
            Assert.That(result.Offer == null);
        }

        [TestCase("Test", "Test", "Test", 0, 0, 0)]
        [TestCase("Test", "Test", "Test", 123, 0, 0)]
        [TestCase("Test", "Test", "Test", 123, 12, 0)]
        [TestCase("Test", "Test", "Test", 123, 12, 42)]
        public async Task HandleAsync_OfferExistsButWithMissingFields_ReturnOfferIncomplete(string title, string city,
            string street, decimal price, double latitude, double longitude)
        {
            var offerId = Guid.NewGuid();

            _offerRepositoryMock.Setup(o => o.GetOffer(It.Is<Guid>(id => id == offerId)))
                .ReturnsAsync(ValidOfferWithMissingFields(offerId, title, city, street, price, latitude, longitude));

            var result = await _queryHandler.HandleAsync(new GetOfferByIdQuery(offerId));

            Assert.That(result != null);
            Assert.That(result.GetType() == typeof(GetOfferByIdQueryResult));
            Assert.That(result.Offer != null);
            Assert.That(result.Offer.GetType() == typeof(ResultModels.Offer));
        }

        private Offer ValidOfferWithAllInformation(Guid offerId)
        {
            return new(offerId, new List<Image>(), "Title", "City", "Street", 123, 23, 23, Guid.NewGuid(),
                new List<OfferType>
                {
                    OfferType.Buy
                });
        }

        private Offer ValidOfferWithMissingFields(Guid offerId, string title, string city, string street, decimal price,
            double latitude, double longitude)
        {
            return new(offerId, new List<Image>(), title, city, street, price, latitude, longitude, Guid.NewGuid(),
                new List<OfferType>
                {
                    OfferType.Buy
                });
        }
    }
}