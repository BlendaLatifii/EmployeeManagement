using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
            public static async Task SeedAsync(AppDbContext context, UserManager<User> userManager)
            {
               await context.Database.EnsureDeletedAsync(CancellationToken.None);
               await context.Database.EnsureCreatedAsync(CancellationToken.None);

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role { Name = "Admin", NormalizedName = "ADMIN", Key = Domain.Enum.Role.Admin },
                        new Role { Name = "Employee", NormalizedName = "EMPLOYEE", Key = Domain.Enum.Role.Employee }
                    );
                    await context.SaveChangesAsync(CancellationToken.None);
                }

                // Seed default admin user
                if (!userManager.Users.Any())
                {
                    var adminUser = new User
                    {
                        UserName = "admin",
                        LastName ="admin",
                        Email = "admin@example.com",
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(adminUser, "Admin123!");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
