using Api.Infrastructure.Auth.Models;

namespace Api.Infrastructure.Auth
{
    public class ReleaseDevelopmentKeysProvider : IJwtKeysProvider
    {
        public JwtKeys Keys { get; }

        //TODO: implement it.
        public ReleaseDevelopmentKeysProvider()
        {
            Keys = new JwtKeys("", "");
        }
    }
}