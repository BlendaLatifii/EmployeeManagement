using Domain.Interface.Security;
using System.Security.Claims;

namespace Presentation.Security
{
    public class ClaimsPrincipalAuthorizationManager : IAuthorizationManager
    {
        private readonly ClaimsPrincipal claimsPrincipal;

        public ClaimsPrincipalAuthorizationManager(IHttpContextAccessor httpContextAccessor)
        {
            this.claimsPrincipal = httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
        }
        public Guid? GetUserId()
        {
            string? id = claimsPrincipal.Identity?.Name;

            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid parsedId))
            {
                return parsedId;
            }

            return null;

        }
    }
}
