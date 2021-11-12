using System;
using Api.Core.Const;

namespace Api.Application.Handlers.Commands
{
    public class CreateClientCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
    }
}