using Api.Infrastructure.Auth.Models;

namespace Api.Infrastructure.Auth
{
    /// <summary>
    /// Provides jwt token keys.
    /// </summary>
    public interface IJwtKeysProvider
    {
        JwtKeys Keys { get; }
    }
}