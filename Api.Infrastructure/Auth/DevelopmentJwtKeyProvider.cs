using Api.Infrastructure.Auth.Models;
using Microsoft.Extensions.Configuration;

namespace Api.Infrastructure.Auth
{
    public class DevelopmentJwtKeyProvider : IJwtKeysProvider
    {
        public JwtKeys Keys { get; }

        public DevelopmentJwtKeyProvider(string accessKey, string refreshKey)
        {
            Keys = new JwtKeys(accessKey, refreshKey);
        }
    }
}