using Domain.Constants;
using Domain.Entities;
using Infrastructure.Configuration.Abstraction;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configuration
{
    public class DepartmentConfiguration : SoftDeletableConfiguration<Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);

            builder.Property(department => department.Name).IsRequired().HasMaxLength(MaxLength.Short);

            builder.Property(department => department.Description).IsRequired().HasMaxLength(MaxLength.Large);
        }
    }
}
