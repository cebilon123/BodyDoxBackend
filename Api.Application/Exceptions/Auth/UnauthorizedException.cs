using System;

namespace Api.Application.Exceptions.Auth
{
    public class UnauthorizedException : ApplicationException
    {
        public override string Code => "Unauthorized_user_exception";

        public UnauthorizedException(string message) : base($"User unauthorized: \n{message}")
        {
        }
    }
}