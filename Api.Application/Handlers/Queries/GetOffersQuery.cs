using System.Collections.Generic;
using Api.Application.Common;
using Api.Application.ResultModels;
using Api.Core.Const;

namespace Api.Application.Handlers.Queries
{
    public class GetOffersQuery : IQuery<ICollection<Offer>>
    {
        public string City { get; }
        public double? Latitude { get; }
        public double? Longitude { get; }
        public IEnumerable<OfferType> OfferTypes { get; }
        public Pagination Page { get; }
        public int? Distance { get; }

        public GetOffersQuery(string city, double? latitude, double? longitude, IEnumerable<OfferType> offerTypes,
            Pagination page, int? distance)
        {
            City = city;
            Latitude = latitude;
            Longitude = longitude;
            OfferTypes = offerTypes;
            Page = page;
            Distance = distance;
        }
    }
}