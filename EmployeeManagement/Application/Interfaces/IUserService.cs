using Application.Dtos;
using Application.Dtos.Users;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDetailsDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<UserDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<UserDetailsDto> AddAsync(SaveUserDto model, CancellationToken cancellationToken);
        Task<UserDetailsDto> UpdateUserAsync(UserDetailsDto model, CancellationToken cancellationToken);
        Task<AuthenticationDto> LoginAsync(LoginDto loginModel, CancellationToken cancellationToken);
    }
}
