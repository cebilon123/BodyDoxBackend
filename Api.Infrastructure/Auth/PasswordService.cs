using System.Security.Cryptography;
using System.Text;
using Api.Core.Auth;
using Api.Core.ValueTypes;
using Isopoh.Cryptography.Argon2;

namespace Api.Infrastructure.Auth
{
    /// <summary>
    /// Argon2 password service.
    /// </summary>
    public class PasswordService : IPasswordService
    {
        private const int DegreeOfParallelism = 8;
        private const int Iterations = 4;
        private const int MemorySize = 1024 * 1024;

        public Password HashPassword(Password password)
        {
            return Argon2.Hash(password);
        }

        public bool AreTheSamePasswords(Password hashedPassword, Password password)
        {
            return Argon2.Verify(hashedPassword, password);
        }
    }
}