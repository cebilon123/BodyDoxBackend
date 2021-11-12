using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Application.Common;
using Api.Application.Handlers.Queries;
using Api.Application.ResultModels;
using Api.Core.Repositories;

namespace Api.Application.Handlers.QueryHandlers
{
    public class GetOffersQueryHandler : IQueryHandler<GetOffersQuery, ICollection<Offer>>
    {
        private readonly IOfferRepository _offerRepository;

        public GetOffersQueryHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<ICollection<Offer>> HandleAsync(GetOffersQuery query)
        {
            var offers = (await _offerRepository.GetOffers(
                null,
                query.City,
                query.Longitude,
                query.Latitude,
                query.Distance,
                query.OfferTypes,
                query.Page.PageNumber,
                query.Page.ResultsAmount
            )).AsResultModelCollection();

            return offers;
        }
    }
}