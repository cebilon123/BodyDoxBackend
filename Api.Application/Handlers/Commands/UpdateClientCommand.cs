using System;
using System.Text.Json.Serialization;
using Api.Core.Const;

namespace Api.Application.Handlers.Commands
{
    public class UpdateClientCommand : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }

        public UpdateClientCommand WithId(Guid id)
        {
            Id = id;
            return this;
        }
    }
}