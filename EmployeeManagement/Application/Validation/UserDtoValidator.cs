using Application.Dtos.Users;
using Domain.Constants;
using FluentValidation;

namespace Application.Validation
{
    public class UserDtoValidator : AbstractValidator<SaveUserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().MaximumLength(MaxLength.Short);
            RuleFor(x => x.UserName).NotEmpty().NotNull().MaximumLength(MaxLength.Short);
            RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(MaxLength.Short);
            RuleFor(x => x.Roles).Must(x => x.Any());
        }
    }
}
