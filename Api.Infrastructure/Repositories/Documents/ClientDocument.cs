using System;
using Api.Core.Const;

namespace Api.Infrastructure.Repositories.Documents
{
    public class ClientDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid AvatarGuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
    }
}