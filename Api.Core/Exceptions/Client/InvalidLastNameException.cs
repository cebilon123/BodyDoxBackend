namespace Api.Core.Exceptions.Client
{
    public class InvalidLastNameException : DomainException
    {
        public override string Code => "invalid_lastname";
        
        public InvalidLastNameException(string lastName) : base($"Lastname is invalid or empty: {lastName}")
        {
        }
    }
}