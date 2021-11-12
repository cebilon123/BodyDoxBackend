using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Api.Core.Domain;
using Api.Core.ValueTypes;
using Api.Infrastructure.Repositories.Documents;
using MongoDB.Driver.GeoJsonObjectModel;
using Image = Api.Core.ValueTypes.Image;

namespace Api.Infrastructure.Repositories
{
    public static class MappingExtensions
    {
        public static UserDocument AsDocument(this User user)
        {
            return new()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                CreatedAtUtc = user.CreatedAtUtc,
                ModifiedAtUtc = user.ModifiedAtUtc
            };
        }

        public static User AsEntity(this UserDocument document)
        {
            return new(document.Id, document.Email, new Password(document.Password),
                document.CreatedAtUtc, document.ModifiedAtUtc, document.FirstName, document.LastName,
                document.PhoneNumber);
        }

        public static UserSessionDocument AsDocument(this UserSession session)
        {
            return new()
            {
                Id = session.Id,
                CreatedAt = session.CreatedAt,
                UserId = session.UserId,
                Token = session.Token,
                RefreshToken = session.RefreshToken
            };
        }

        public static UserSession? AsEntity(this UserSessionDocument? document)
        {
            return document == null
                ? null
                : new UserSession(document.Id, document.UserId, document.CreatedAt, document.Token,
                    document.RefreshToken);
        }

        public static Image AsEntity(this Documents.Image image)
        {
            return new(image.Url, image.ImageAlt);
        }

        public static Offer? AsEntity(this OfferDocument? document) =>
            document == null
                ? null
                : new Offer(
                    document.Id,
                    document.Images.Select(i => new Image(i.Url, i.ImageAlt)).ToList(),
                    document.Title,
                    document.City,
                    document.Street,
                    document.Price,
                    document.Location.Latitude,
                    document.Location.Longitude,
                    document.AuthorId,
                    document.OfferTypes);

        public static ICollection<Offer> AsEntityCollection(this IEnumerable<OfferDocument> documents)
        {
            var col = new List<Offer>();
            foreach (var document in documents) col.Add(document.AsEntity());

            return col;
        }

        public static OfferDocument AsDocument(this Offer entity)
        {
            return new()
            {
                Id = entity.Id,
                AuthorId = entity.AuthorId,
                City = entity.City,
                Street = entity.Street,
                Price = entity.Price,
                Title = entity.Title,
                OfferTypes = entity.OfferTypes,
                Location = new GeoJson2DGeographicCoordinates(entity.Longitude, entity.Latitude),
                Images = entity.Images.Select(i => new Documents.Image(i.Url, i.ImageAlt)).ToList()
            };
        }
    }
}