namespace Api.Application.Exceptions.User
{
    public class PasswordsDontMatchException : ApplicationException
    {
        public override string Code => "Passwords_dont_match";

        public PasswordsDontMatchException() : base("Passwords don't match")
        {
        }
    }
}