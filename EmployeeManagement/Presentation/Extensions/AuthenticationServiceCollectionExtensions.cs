using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation.Helpers;

namespace Presentation.Extensions
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddCostomAuthentication(this IServiceCollection services, string apiSecret)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => 
                { 
                    options.TokenValidationParameters = TokenHelpers.GetValidationParameters(apiSecret);
                });
        }
    }
}
