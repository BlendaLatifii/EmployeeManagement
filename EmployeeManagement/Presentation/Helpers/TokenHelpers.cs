using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Helpers
{
    public static class TokenHelpers
    {
        public static TokenValidationParameters GetValidationParameters(string apiSecret)
        {
            byte[] key = Encoding.ASCII.GetBytes(apiSecret);

            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        }

        public static string CreateToken(IEnumerable<Claim> claims, int expirationMinutes, string tokenSecret, string tokenIssure, string tokenAudience)
        {
            byte[] key = Encoding.ASCII.GetBytes(tokenSecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            var subject = new ClaimsIdentity(claims);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = tokenIssure,
                Audience = tokenAudience,
                Subject = subject,
                Expires = DateTime.Now.AddMinutes(expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
