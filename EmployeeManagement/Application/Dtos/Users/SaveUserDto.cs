using Domain.Enum;

namespace Application.Dtos.Users
{
    public class SaveUserDto : UserDto
    {
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get;set; } = string.Empty;
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
