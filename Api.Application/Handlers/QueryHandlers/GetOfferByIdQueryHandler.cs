using System.Threading.Tasks;
using Api.Application.Common;
using Api.Application.Handlers.Queries;
using Api.Core.Repositories;

namespace Api.Application.Handlers.QueryHandlers
{
    public class GetOfferByIdQueryHandler : IQueryHandler<GetOfferByIdQuery, GetOfferByIdQueryResult>
    {
        private readonly IOfferRepository _offerRepository;

        public GetOfferByIdQueryHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<GetOfferByIdQueryResult> HandleAsync(GetOfferByIdQuery query)
        {
            var offer = await _offerRepository.GetOffer(query.Id);

            return new GetOfferByIdQueryResult(offer.AsResultModel());
        }
    }
}