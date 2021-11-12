using System;
using Api.Core.ValueTypes;

namespace Api.Application.Handlers.Queries
{
    public class SignInQuery : IQuery<SignInQueryResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInQueryResult
    {
        public AccessToken AccessToken { get; }
        public RefreshToken RefreshToken { get; }

        public SignInQueryResult(AccessToken accessToken, RefreshToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}