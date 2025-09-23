using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(user => user.UserName)
                .IsRequired().HasMaxLength(MaxLength.Short);

            builder.Property(user => user.LastName)
              .IsRequired().HasMaxLength(MaxLength.Short);

            builder.Property(user => user.Email).IsRequired().HasMaxLength(MaxLength.Short);

            builder.HasIndex(user => user.Email).IsUnique();
        }
    }
}
