using System;
using System.Collections.Generic;
using System.Security.Claims;
using Api.Core.ValueTypes;
using Microsoft.VisualBasic.CompilerServices;

namespace Api.Core.Auth
{
    public interface IJwtService
    {
        AccessToken GenerateToken(IEnumerable<KeyValuePair<string, object>> claims);
        RefreshToken GenerateRefreshToken(IEnumerable<KeyValuePair<string, object>> claims);
        Token DecodeToken(string token);
        Token DecodeRefreshToken(RefreshToken token);
    }
}