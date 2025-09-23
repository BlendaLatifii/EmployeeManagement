using Microsoft.IdentityModel.Tokens;
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
    }
}
