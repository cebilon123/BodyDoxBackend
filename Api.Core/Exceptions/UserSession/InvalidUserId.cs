namespace Api.Core.Exceptions.UserSession
{
    public class InvalidUserId : DomainException
    {
        public override string Code => "invalid_user_id";

        public InvalidUserId(string id) : base($"User id was empty or invalid: {id}")
        {
        }
    }
}