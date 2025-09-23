using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Domain.Enum.Role Key { get; set; }
        public List<UserRole> UserRoles = new List<UserRole>();
    }
}
