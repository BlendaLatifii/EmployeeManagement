using Application.Dtos.Users;
using Domain.Constants;
using Domain.Interface.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation
{
    public class UpdatedUserDtoValidator :AbstractValidator<UserDetailsDto>
    {
        public UpdatedUserDtoValidator(IUserRepository userRepository)
        {
            RuleFor(user => user.Id).NotEmpty().NotNull();
            RuleFor(user => user.Email).NotEmpty().NotNull().MaximumLength(MaxLength.Short);
            RuleFor(user => user).MustAsync(async (dto, cancellationToken) =>
            {
                var id = dto?.Id ?? Guid.Empty;
                bool exists = await userRepository.Query().Where(x => x.Email == dto.Email && dto.Id != id).AnyAsync(cancellationToken);
                return !exists;
            }).WithMessage("Email must be unique");
            RuleFor(user => user.UserName).NotEmpty().NotNull().MaximumLength(MaxLength.Short);
            RuleFor(user => user.LastName).NotEmpty().NotNull().MaximumLength(MaxLength.Short);
        }
    }
}
