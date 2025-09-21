
namespace Application.Dtos.Users
{
    public class UserDto
    {
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}
