using System;
using System.Collections.Generic;
using Api.Core.Auth;
using Api.Core.ValueTypes;
using JWT.Algorithms;
using JWT.Builder;

namespace Api.Infrastructure.Auth
{
    public class JwtService : IJwtService
    {
        private const int TokenActiveTimeMinutes = 15;
        private const int RefreshTokenActiveTimeMinutes = 120;

        private readonly IJwtKeysProvider _keysProvider;

        public JwtService(IJwtKeysProvider keysProvider)
        {
            _keysProvider = keysProvider;
        }

        public AccessToken GenerateToken(IEnumerable<KeyValuePair<string, object>> claims)
        {
            return JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(_keysProvider.Keys.TokenKey)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(TokenActiveTimeMinutes).ToUnixTimeSeconds())
                .AddClaims(claims)
                .Encode();
        }

        public RefreshToken GenerateRefreshToken(IEnumerable<KeyValuePair<string, object>> claims)
        {
            return JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(_keysProvider.Keys.RefreshTokenKey)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(RefreshTokenActiveTimeMinutes).ToUnixTimeSeconds())
                .AddClaims(claims)
                .Encode();
        }

        public Token DecodeToken(string token)
        {
            return JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(_keysProvider.Keys.TokenKey)
                .Decode<Token>(token);
        }

        public Token DecodeRefreshToken(RefreshToken token)
        {
            return JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(_keysProvider.Keys.RefreshTokenKey)
                .Decode<Token>(token);
        }
    }
}