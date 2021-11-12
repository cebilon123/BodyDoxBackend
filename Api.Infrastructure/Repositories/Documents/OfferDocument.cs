using System;
using System.Collections.Generic;
using Api.Core.Const;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Infrastructure.Repositories.Documents
{
    public class OfferDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public List<Image>? Images { get; set; }
        public string? Title { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public decimal Price { get; set; }
        public List<OfferType>? OfferTypes { get; set; }
        public GeoJson2DGeographicCoordinates? Location { get; set; }
    }

    public class Image
    {
        [BsonRepresentation(BsonType.String)] public string Url { get; set; }
        [BsonRepresentation(BsonType.String)] public string ImageAlt { get; set; }

        public Image(string image, string imageAlt)
        {
            Url = image;
            ImageAlt = imageAlt;
        }
    }
}