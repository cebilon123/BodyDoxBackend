namespace Api.Core.Exceptions.Client
{
    public class InvalidFirstNameException : DomainException
    {
        public override string Code => "invalid_firstname";
        
        public InvalidFirstNameException(string firstName) : base($"Firstname is invalid or empty: {firstName}")
        {
        }
    }
}