using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(department => department.Name).IsRequired().HasMaxLength(MaxLength.Short);

            builder.Property(department => department.Description).IsRequired().HasMaxLength(MaxLength.Large);
        }
    }
}
