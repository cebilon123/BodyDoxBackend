using System;
using Api.Core.Const;
using Api.Core.Exceptions.Client;

namespace Api.Core.Domain
{
    public class Client
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }
        public string Address { get; }
        public string ZipCode { get; }
        public string City { get; }
        public string PhoneNumber { get; }
        public Gender Gender { get; }
        public Guid? AvatarGuid{ get; }

        public Client(Guid id, string firstName, string lastName, string email, DateTime birthDate, string address,
            string zipCode, string city, string phoneNumber, Gender gender = Gender.None, Guid? avatarGuid = null)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new InvalidFirstNameException(firstName);

            if (string.IsNullOrEmpty(lastName))
                throw new InvalidLastNameException(lastName);

            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            Address = address;
            ZipCode = zipCode;
            City = city;
            PhoneNumber = phoneNumber;
            Gender = gender;
            AvatarGuid = avatarGuid;
        }
    }
}