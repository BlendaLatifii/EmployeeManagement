using Application.Dtos;
using Domain.Constants;
using FluentValidation;

namespace Application.Validation
{
    public class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(MaxLength.Short);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(MaxLength.Large);
        }
    }
}
