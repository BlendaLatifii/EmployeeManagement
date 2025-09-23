using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
            public static async Task SeedAsync(AppDbContext context, UserManager<User> userManager, DbInitializerConfig config)
            {
               await context.Database.EnsureDeletedAsync(CancellationToken.None);
               await context.Database.EnsureCreatedAsync(CancellationToken.None);

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role { Name = RoleConstants.Admin, NormalizedName = RoleConstants.Admin.ToUpper(), Key = Domain.Enum.Role.Admin },
                        new Role { Name = RoleConstants.Employee, NormalizedName = RoleConstants.Employee.ToUpper(), Key = Domain.Enum.Role.Employee }
                    );
                    await context.SaveChangesAsync(CancellationToken.None);
                }

                // Seed default admin user
                if (!userManager.Users.Any())
                {
                    var adminUser = new User
                    {
                        UserName = "admin",
                        LastName = "admin",
                        Email = config.Admin.Email,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(adminUser, config.Admin.Password);
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                
                    var employeeUser = new User
                    {
                        UserName = "employee",
                        LastName = "employee",
                        Email = config.Employee.Email,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(employeeUser, config.Employee.Password);
                    await userManager.AddToRoleAsync(employeeUser, RoleConstants.Employee);
                    await context.SaveChangesAsync(CancellationToken.None);
            }
            }
        }
    }
