using System;
using Api.Core.ValueTypes;

namespace Api.Core.Auth
{
    public interface IPasswordService
    {
        Password HashPassword(Password password);
        bool AreTheSamePasswords(Password hashedPassword, Password password);
    }
}