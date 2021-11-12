using System;
using Api.Core.Exceptions.User;
using Api.Core.ValueTypes;

namespace Api.Core.Domain
{
    public class User
    {
        public Guid Id { get; }
        public Email Email { get; }
        public Password Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string PhoneNumber { get; }
        public DateTime CreatedAtUtc { get; }
        public DateTime ModifiedAtUtc { get; }

        public User(Guid id, Email email, Password password, DateTime createdAtUtc, DateTime modifiedAtUtc,
            string firstName, string lastName, string phoneNumber)
        {
            if (string.IsNullOrEmpty(email))
                throw new InvalidEmailException(email);

            if (string.IsNullOrEmpty(password))
                throw new InvalidPasswordException();

            Id = id;
            Email = email;
            Password = password;
            CreatedAtUtc = createdAtUtc;
            ModifiedAtUtc = modifiedAtUtc;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
    }
}