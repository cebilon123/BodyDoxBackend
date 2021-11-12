using System;
using System.Collections.Generic;
using Api.Core.Const;
using Api.Core.Exceptions.Offer;
using Api.Core.ValueTypes;

namespace Api.Core.Domain
{
    public class Offer
    {
        public Guid Id { get; }
        public Guid AuthorId { get; }
        public List<Image> Images { get; }
        public string Title { get; }
        public string City { get; }
        public string Street { get; }
        public decimal Price { get; }
        public Latitude Latitude { get; }
        public Longitude Longitude { get; }
        public List<OfferType> OfferTypes { get; }

        public Offer(Guid id, List<Image> images, string title, string city, string street, decimal price,
            Latitude latitude, Longitude longitude, Guid authorId, List<OfferType> types)
        {
            if (!latitude.IsValid()) throw new InvalidLatitudeException(latitude);

            if (!longitude.IsValid()) throw new InvalidLongitudeException(longitude);

            if (string.IsNullOrEmpty(city)) throw new InvalidCityException(city);

            if (string.IsNullOrEmpty(title)) throw new InvalidTitleException(title);

            if (string.IsNullOrEmpty(street)) throw new InvalidStreetException(street);

            Id = id;
            Images = images;
            Title = title;
            City = city;
            Street = street;
            Price = price;
            Latitude = latitude;
            Longitude = longitude;
            AuthorId = authorId;
            OfferTypes = types;
        }
    }
}