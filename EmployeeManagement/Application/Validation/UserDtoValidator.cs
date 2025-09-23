using Application.Dtos.Users;
using Domain.Constants;
using FluentValidation;

namespace Application.Validation
{
    public class UserDtoValidator : AbstractValidator<SaveUserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Email).NotEmpty().NotNull().MaximumLength(MaxLength.Short);
            RuleFor(user => user.UserName).NotEmpty().NotNull().MaximumLength(MaxLength.Short);
            RuleFor(user => user.LastName).NotEmpty().NotNull().MaximumLength(MaxLength.Short);
            RuleFor(user => user.Roles).Must(role => role.Any());
        }
    }
}
