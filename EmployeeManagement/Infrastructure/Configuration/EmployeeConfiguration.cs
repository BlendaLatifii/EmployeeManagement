using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(employee => employee.Id);

            builder.Property(employee => employee.Name)
                .IsRequired().HasMaxLength(MaxLength.Short);

            builder.Property(employee => employee.Surname)
              .IsRequired().HasMaxLength(MaxLength.Short);

            builder.Property(employee => employee.Email).IsRequired().HasMaxLength(MaxLength.Short);

            builder.HasIndex(employee => employee.Email).IsUnique();

            builder.Property(employee => employee.DateOfJoining).IsRequired();

            builder.HasOne<Department>(employee => employee.Department)
                .WithMany(employee => employee.Employees)
                .HasForeignKey(employee => employee.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
