using System.Linq;
using Api.Core.Domain;
using Api.Core.ValueTypes;
using Api.Infrastructure.Repositories.Documents;
using Annotation = Api.Infrastructure.Repositories.Documents.Annotation;
using Image = Api.Infrastructure.Repositories.Documents.Image;
using Position = Api.Infrastructure.Repositories.Documents.Position;
using Resolution = Api.Infrastructure.Repositories.Documents.Resolution;

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

        public static ClientDocument AsDocument(this Client client)
            => new ClientDocument()
            {
                Id = client.Id,
                Address = client.Address,
                City = client.City,
                Email = client.Email,
                Gender = client.Gender,
                AvatarGuid = client.AvatarGuid,
                BirthDate = client.BirthDate,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                ZipCode = client.ZipCode,
                CreatedBy = client.CreatedBy,
                CreatedDate = client.CreatedDate,
                DeletedDate = client.DeletedDate,
                UpdatedDate = client.UpdatedDate
            };

        public static Client? AsEntity(this ClientDocument? document)
            => document == null
                ? null
                : new Client(document.Id, document.FirstName, document.LastName, document.Email, document.BirthDate,
                    document.Address, document.ZipCode, document.City, document.PhoneNumber, document.CreatedDate,
                    document.CreatedBy, document.Gender, document.AvatarGuid, document.UpdatedDate,
                    document.DeletedDate);

        public static SessionDocument AsDocument(this Session session)
            => new SessionDocument()
            {
                Id = session.Id,
                ClientId = session.ClientId,
                CreatedDate = session.CreatedDate,
                UpdatedDate = session.UpdatedDate,
                ImagesData = session.Images.Select(i => i.AsDocument()).ToList()
            };

        public static Image AsDocument(this Api.Core.Domain.Image image)
            => new Image()
            {
                Id = image.Id,
                Side = image.Side,
                Annotations = image.Annotations.Select(a => a.AsDocument()).ToList()
            };

        public static Annotation AsDocument(this Api.Core.Domain.Annotation annotation)
            => new Annotation()
            {
                Id = annotation.Id,
                Position = annotation.Position.AsDocument(),
                Rotation = annotation.Rotation,
                Scale = annotation.Scale,
                Type = annotation.Type,
                MarkerEnd = annotation.MarkerEnd.AsDocument(),
                MarkerStart = annotation.MarkerStart.AsDocument()
            };

        public static Position AsDocument(this Api.Core.Domain.Position position)
            => new Position() { X = position.X, Y = position.Y };

        public static Session? AsEntity(this SessionDocument document)
            => document == null
                ? null
                : new Session(
                    document.Id,
                    document.ClientId,
                    document.ImagesData.Select(i => i.AsEntity()).ToList(),
                    document.CreatedDate,
                    document.UpdatedDate);

        public static Api.Core.Domain.Image? AsEntity(this Image document)
            => document == null
                ? null
                : new Core.Domain.Image(
                    document.Id, 
                    document.Resolution.AsEntity(),
                    document.Annotations.Select(a => a.AsEntity()).ToList(), 
                    document.Side);

        public static Api.Core.Domain.Resolution? AsEntity(this Resolution document)
            => document == null
                ? null
                : new Core.Domain.Resolution() { Height = document.Height, Width = document.Width };

        public static Api.Core.Domain.Annotation? AsEntity(this Annotation document)
            => document == null
                ? null
                : new Core.Domain.Annotation(
                    document.Id, 
                    document.Type, 
                    document.MarkerStart.AsEntity(),
                    document.MarkerEnd.AsEntity(),
                    document.Position.AsEntity(), 
                    document.Rotation, 
                    document.Scale);

        public static Core.Domain.Position? AsEntity(this Position? document)
            => document == null ? null : new Core.Domain.Position() { X = document.X, Y = document.Y };
    }
}