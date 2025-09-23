using Domain.Constants;
using Domain.Entities;
using Infrastructure.Configuration.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class EmployeeConfiguration : SoftDeletableConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.HasKey(employee => employee.Id);

            builder.Property(employee => employee.DateOfJoining).IsRequired();

            builder.HasOne(employee => employee.Department)
                .WithMany(employee => employee.Employees)
                .HasForeignKey(employee => employee.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(employee => employee.User)
               .WithOne(user => user.Employee)
               .HasForeignKey<Employee>(employe => employe.UserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
