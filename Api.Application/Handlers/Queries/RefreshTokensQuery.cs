using Api.Core.ValueTypes;

namespace Api.Application.Handlers.Queries
{
    public class RefreshTokensQuery : IQuery<RefreshTokensQueryResult>
    {
        public RefreshToken RefreshToken { get; }

        public RefreshTokensQuery(RefreshToken refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }

    public class RefreshTokensQueryResult
    {
        public AccessToken AccessToken { get; }
        public RefreshToken RefreshToken { get; }

        public RefreshTokensQueryResult(AccessToken accessToken, RefreshToken refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}