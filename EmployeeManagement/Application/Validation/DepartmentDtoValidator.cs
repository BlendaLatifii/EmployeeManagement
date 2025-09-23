using Application.Dtos.Department;
using Domain.Constants;
using FluentValidation;

namespace Application.Validation
{
    public class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentDtoValidator()
        {
            RuleFor(department => department.Name).NotEmpty().MaximumLength(MaxLength.Short);
            RuleFor(department => department.Description).NotEmpty().MaximumLength(MaxLength.Large);
        }
    }
}
