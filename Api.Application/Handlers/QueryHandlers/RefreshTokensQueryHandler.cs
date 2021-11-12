using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Exceptions.Auth;
using Api.Application.Handlers.Queries;
using Api.Core.Auth;
using Api.Core.Domain;
using Api.Core.Repositories;

namespace Api.Application.Handlers.QueryHandlers
{
    public class RefreshTokensQueryHandler : IQueryHandler<RefreshTokensQuery, RefreshTokensQueryResult>
    {
        private readonly IJwtService _jwtService;
        private readonly IUserSessionsRepository _userSessionsRepository;

        public RefreshTokensQueryHandler(IJwtService jwtService, IUserSessionsRepository userSessionsRepository)
        {
            _jwtService = jwtService;
            _userSessionsRepository = userSessionsRepository;
        }

        public async Task<RefreshTokensQueryResult> HandleAsync(RefreshTokensQuery query)
        {
            try
            {
                if (string.IsNullOrEmpty(query.RefreshToken))
                    throw new UnauthorizedException("Refresh token was empty");

                var token = _jwtService.DecodeRefreshToken(query.RefreshToken);

                var sessionId = Guid.NewGuid();
                var claims = new Dictionary<string, object>()
                {
                    {"id", token.Id},
                    {"businessId", "implement me after business"}, //TODO implement after business
                    {"sessionId", sessionId}
                };

                var accessToken = _jwtService.GenerateToken(claims);
                var refreshToken = _jwtService.GenerateRefreshToken(claims);

                await _userSessionsRepository.Insert(new UserSession(sessionId, token.Id,
                    DateTime.Now.ToUniversalTime(), accessToken, refreshToken));

                return new RefreshTokensQueryResult(accessToken, refreshToken);
            }
            catch (Exception ex)
            {
                throw new UnauthorizedException(ex.Message);
            }
        }
    }
}