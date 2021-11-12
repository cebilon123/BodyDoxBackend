namespace Api.Core.Exceptions.User
{
    public class InvalidEmailException : DomainException
    {
        public override string Code => "invalid_email";

        public InvalidEmailException(string email) : base($"Invalid email: {email}")
        {
        }
    }
}