using System.Threading.Tasks;
using Api.Application.Exceptions.Auth;
using Api.Core.Auth;
using Api.Core.Repositories;
using Api.Infrastructure.Auth.Models;
using JWT.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Api.Infrastructure.Auth
{
    public class JwtCookiesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserSessionsRepository _sessionsRepository;

        public JwtCookiesMiddleware(RequestDelegate next, IUserSessionsRepository sessionsRepository)
        {
            _next = next;
            _sessionsRepository = sessionsRepository;
        }

        public async Task Invoke(HttpContext context, IRequestInfoProvider requestInfoProvider, IJwtService jwtService)
        {
            var token = context.Request.Cookies[Const.AccessTokenName];

            try
            {
                if (!string.IsNullOrEmpty(token) && jwtService.DecodeToken(token) is var decodeToken)
                    if (await _sessionsRepository.GetSession(decodeToken.SessionId) != null)
                    {
                        context.Items.Add("valid_token", decodeToken);
                        context.Items.Add(AuthConst.UserId, decodeToken.Id);
                    }
            }
            catch (SignatureVerificationException ex)
            {
                throw new UnauthorizedException(ex.Message);
            }

            await _next(context);
        }
    }
}