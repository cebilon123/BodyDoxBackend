namespace Api.Core.Exceptions.User
{
    public class EmailOccupiedException : DomainException
    {
        public override string Code => "email_occupied";

        public EmailOccupiedException(string email) : base($"Email occupied: {email}")
        {
        }
    }
}