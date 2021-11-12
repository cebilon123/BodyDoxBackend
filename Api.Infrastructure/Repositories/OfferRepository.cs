using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Core.Common.Extensions;
using Api.Core.Const;
using Api.Core.Domain;
using Api.Core.Repositories;
using Api.Infrastructure.Repositories.Documents;
using MongoDB.Driver;

namespace Api.Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        // Default max distance in meters
        private const int MaxDefaultDistance = 1000;

        private readonly IMongoGenericRepository<OfferDocument, Guid> _repository;

        public OfferRepository(IMongoGenericRepository<OfferDocument, Guid> repository)
        {
            _repository = repository;
        }


        public async Task<Offer> GetOffer(Guid id) =>
            _repository.Collection.AsQueryable().FirstOrDefault(o => o.Id == id).AsEntity();

        public async Task<ICollection<Offer>> GetOffers(Guid? authorId, string city, double? longitude,
            double? latitude, int? maxDistance, IEnumerable<OfferType> types, int page, int resultsPerPage)
        {
            var filterBuilder = new FilterDefinitionBuilder<OfferDocument>();
            var filtersDefinitions = new List<FilterDefinition<OfferDocument>>();

            if (authorId.HasValue)
                filtersDefinitions.Add(filterBuilder.Eq(f => f.AuthorId, authorId.Value));

            if (longitude.HasValue && latitude.HasValue)
                filtersDefinitions.Add(filterBuilder.NearSphere(f => f.Location, latitude.Value, longitude.Value,
                    maxDistance ?? 100, 0));

            if (!string.IsNullOrEmpty(city))
                filtersDefinitions.Add(filterBuilder.Eq(f => f.City, city));

            if (types.Any())
                types.ToList().ForEach(type =>
                {
                    filtersDefinitions.Add(filterBuilder.ElemMatch(f => f.OfferTypes, x => x == type));
                });

            var builtFilter = filtersDefinitions.Any() ? filterBuilder.And(filtersDefinitions) : filterBuilder.Empty;

            return (await _repository.Collection
                    .Find(builtFilter)
                    .Limit(resultsPerPage)
                    .Skip(page)
                    .ToListAsync())
                .AsEntityCollection();
        }

        public async Task Insert(Offer offer)
            => await _repository.AddAsync(offer.AsDocument());

        public Task<bool> OfferExistsForUser(Guid offerId, Guid authorId) =>
            _repository.ExistsAsync(o => o.Id == offerId && o.AuthorId == authorId);

        public async Task RemoveOffer(Guid offerId)
            => await _repository.DeleteAsync(offerId);

        public async Task Update(Offer offer)
            => await _repository.UpdateAsync(offer.AsDocument());

    }
}