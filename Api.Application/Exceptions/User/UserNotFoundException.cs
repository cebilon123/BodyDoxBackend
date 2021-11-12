namespace Api.Application.Exceptions.User
{
    public class UserNotFoundException : ApplicationException
    {
        public override string Code => "user_not_found";

        public UserNotFoundException(string email) : base($"User with mail: {email} not found.")
        {
        }
    }
}