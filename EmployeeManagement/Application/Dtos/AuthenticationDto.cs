using Application.Dtos.Users;

namespace Application.Dtos 
{ 
    public class AuthenticationDto
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public UserDetailsDto UserData { get; set; } = null!;
        public List<string> UserRole { get; set; } = new List<string>();
    }
}