namespace Api.Core.Exceptions.Offer
{
    public class InvalidLatitudeException : DomainException
    {
        public override string Code => "invalid_latitude";

        public InvalidLatitudeException(double value) : base($"Invalid latitude: {value}")
        {
        }
    }
}