using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Interface
{
    public interface IAuditData
    {
        public void Audit(IEnumerable<EntityEntry> entityEntries);
    }
}
