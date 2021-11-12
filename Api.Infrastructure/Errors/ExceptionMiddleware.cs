using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Infrastructure.Errors
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionToResponseMapper _exceptionToResponseMapper;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, IExceptionToResponseMapper exceptionToResponseMapper,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _exceptionToResponseMapper = exceptionToResponseMapper;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next.Invoke(ctx);
            }
            catch (Exception e)
            {
                var response = ctx.Response;
                response.ContentType = "application/json";

                var error = _exceptionToResponseMapper.GetErrorBasedOnException(e);

                _logger.LogError(e, $"Status: {error.StatusCode} | Code: {error.Code} | Message: {error.Message}");

                var result = JsonSerializer.Serialize(new {message = error.Message, code = error.Code});
                response.StatusCode = (int) error.StatusCode;
                await response.WriteAsync(result);
            }
        }
    }
}