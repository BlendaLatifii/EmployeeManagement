using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
           builder.HasOne(userRole=> userRole.User)
                .WithMany(userRole => userRole.Roles)
                .HasForeignKey(userRole => userRole.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(userRole => userRole.Role)
               .WithMany()
               .HasForeignKey(userRole => userRole.RoleId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
