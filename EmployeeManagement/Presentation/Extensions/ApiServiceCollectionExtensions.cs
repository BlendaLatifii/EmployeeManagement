using Application.Interfaces;
using Application.Services;
using Domain.Configs;
using Domain.Interface.Repository;
using Domain.Interface.Security;
using Infrastructure.Repositories;
using Presentation.Security;

namespace Infrastructure.Extensions
{
    public static class ApiServiceCollectionExtensions
    {

        public static void AddApiServices(this IServiceCollection services, ApiConfig apiConfig)
        {

           services.AddSingleton(apiConfig);
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationManager, ClaimsPrincipalAuthorizationManager>();
        }
    }
}
