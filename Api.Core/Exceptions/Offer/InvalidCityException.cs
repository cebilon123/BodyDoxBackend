namespace Api.Core.Exceptions.Offer
{
    public class InvalidCityException : DomainException
    {
        public override string Code => "invalid_city";

        public InvalidCityException(string city) : base($"Invalid city: {city}")
        {
        }
    }
}