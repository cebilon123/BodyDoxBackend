namespace Api.Core.Exceptions.Offer
{
    public class InvalidStreetException : DomainException
    {
        public override string Code => "invalid_street";

        public InvalidStreetException(string street) : base($"Invalid street: {street}")
        {
        }
    }
}