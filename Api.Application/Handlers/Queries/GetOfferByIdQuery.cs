using System;
using Api.Application.ResultModels;

namespace Api.Application.Handlers.Queries
{
    public class GetOfferByIdQuery : IQuery<GetOfferByIdQueryResult>
    {
        public Guid Id { get; }

        public GetOfferByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetOfferByIdQueryResult
    {
        public Offer Offer { get; }

        public GetOfferByIdQueryResult(Offer offer)
        {
            Offer = offer;
        }
    }
}