namespace Application.Dtos.Users
{
    public class UserDetailsDto : UserDto
    {
        public Guid Id { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
