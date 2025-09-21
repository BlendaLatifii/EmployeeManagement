using Domain.Enum;

namespace Application.Dtos.Users
{
    public class SaveUserDto : UserDto
    {
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get;set; } = null!;
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
