using Domain.Interface.Security;
using Domain.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure.Extensions
{
    public static class DbContextServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddScoped<IAuditData, AuditData>();
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
