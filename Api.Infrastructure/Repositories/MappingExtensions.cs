using Api.Core.Domain;
using Api.Core.ValueTypes;
using Api.Infrastructure.Repositories.Documents;

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
    }
}