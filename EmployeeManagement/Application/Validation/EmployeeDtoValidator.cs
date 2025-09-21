using Application.Dtos.Employee;
using Domain.Constants;
using Domain.Interface.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(employee => employee.Name).NotNull().NotEmpty().MaximumLength(MaxLength.Short);
            RuleFor(employee => employee.Surname).NotNull().NotEmpty().MaximumLength(MaxLength.Short);
            RuleFor(employee => employee.DateOfJoining).NotNull().NotEmpty();
            RuleFor(employee => employee.Email).NotNull().NotEmpty().MaximumLength(MaxLength.Short);
            RuleFor(employee => employee.Email).MustAsync(async (email, cancellationToken) =>
            {
                bool exists = await employeeRepository.Query().Where(employee => employee.Email == email).AnyAsync(cancellationToken);
                return !exists;
            }).WithMessage("Email must be unique");
        }
    }
}
