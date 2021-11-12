using System;
using Api.Core.Const;

namespace Api.Infrastructure.Repositories.Documents
{
    public class ClientDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid? AvatarGuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Gender Gender { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid CreatedBy { get; set; }
    }
}