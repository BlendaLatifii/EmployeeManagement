using Domain.Constants;
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

        public Guid? GetDepartmentId()
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimConstants.Department);

            if (claim != null && Guid.TryParse(claim.Value, out Guid departmentId))
            {
                return departmentId;
            }
            return null;

        }
    }
}
