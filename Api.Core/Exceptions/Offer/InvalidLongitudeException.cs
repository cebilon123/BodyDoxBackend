namespace Api.Core.Exceptions.Offer
{
    public class InvalidLongitudeException : DomainException
    {
        public override string Code => "invalid_longitude";

        public InvalidLongitudeException(double value) : base($"Invalid longitude: {value}")
        {
        }
    }
}