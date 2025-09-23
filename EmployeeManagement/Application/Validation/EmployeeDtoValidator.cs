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
            RuleFor(employee => employee.LastName).NotNull().NotEmpty().MaximumLength(MaxLength.Short);
            RuleFor(employee => employee.DateOfJoining).NotNull().NotEmpty();
            RuleFor(employee => employee.Email).NotNull().NotEmpty().MaximumLength(MaxLength.Short);
            RuleFor(employee => employee).MustAsync(async (dto, cancellationToken) =>
            {
                var id = (dto as EmployeeDetailDto)?.Id ?? Guid.Empty;
                bool exists = await employeeRepository.Query().Where(employee => employee.User.Email == dto.Email && employee.Id != id).AnyAsync(cancellationToken);
                return !exists;
            }).WithMessage("Email must be unique");
            RuleFor(employee => employee.DepartmentId).NotNull().NotEmpty();
        }
    }
}
