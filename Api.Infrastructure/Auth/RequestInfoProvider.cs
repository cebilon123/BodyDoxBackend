using System;
using System.Threading.Tasks;
using Api.Core.Auth;
using Api.Core.Domain;
using Api.Infrastructure.Errors.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Auth
{
    public class RequestInfoProvider : IRequestInfoProvider
    {
        private readonly IServiceScopeFactory _serviceFactory;
        private readonly IHttpContextAccessor? _contextAccessor;

        public Guid UserId => (Guid) (_contextAccessor?.HttpContext.Items[AuthConst.UserId] ??
                                      throw new InfrastructureException("UserId in context was null. Called from: ",
                                          typeof(RequestInfoProvider)));

        public RequestInfoProvider(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            using var scope = _serviceFactory.CreateScope();
            _contextAccessor = scope.ServiceProvider.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
        }
    }
}