using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string LastName { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public List<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}
