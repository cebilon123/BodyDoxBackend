namespace Api.Infrastructure.Auth.Models
{
    public class JwtKeys
    {
        public string TokenKey { get; }
        public string RefreshTokenKey { get; }

        public JwtKeys(string tokenKey, string refreshTokenKey)
        {
            TokenKey = tokenKey;
            RefreshTokenKey = refreshTokenKey;
        }
    }
}