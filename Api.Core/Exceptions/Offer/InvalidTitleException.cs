namespace Api.Core.Exceptions.Offer
{
    public class InvalidTitleException : DomainException
    {
        public override string Code => "invalid_title";

        public InvalidTitleException(string title) : base($"Invalid title: {title}")
        {
        }
    }
}