using Domain.Entities.Abstraction;
using Domain.Interface.Security;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Domain.Interface;

namespace Infrastructure.Data
{
    public class AuditData : IAuditData
    {
        private readonly IAuthorizationManager _authorizationManager;
        private Guid UserId { get => _authorizationManager.GetUserId() ?? throw new NotImplementedException(); }
        public AuditData(IAuthorizationManager authorizationManager)
        {
            _authorizationManager = authorizationManager;
        }
        public void Audit(IEnumerable<EntityEntry> entityEntries)
        {
            DateTime dateTime = DateTime.Now;

            foreach (var entry in entityEntries.ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        SetCreateDateUser(entry, dateTime);
                        SetUpdateDateUser(entry, dateTime);
                        break;
                    case EntityState.Modified:
                        SetUpdateDateUser(entry, dateTime);
                        break;
                    case EntityState.Deleted:
                        SetDeleteDateUser(entry, dateTime);
                        break;
                }
            }
        }
        private void SetCreateDateUser(EntityEntry entry, DateTime timeStamp)
        {
            if (entry.Entity is IHasCreateUser hasCreateDate)
            {
                hasCreateDate.CreateDate = timeStamp;
            }
            if (entry.Entity is IHasCreateUser hasCreateUser)
            {
                hasCreateUser.CreateUserId = UserId;
            }
        }
        private void SetUpdateDateUser(EntityEntry entry, DateTime timeStamp)
        {
            if (entry.Entity is IHasUpdateUser hasUpdateDate)
            {
                hasUpdateDate.UpdateDate = timeStamp;
            }
            if (entry.Entity is IHasUpdateUser hasUpdateUser)
            {
                hasUpdateUser.UpdateUserId = UserId;
            }
        }

        private void SetDeleteDateUser(EntityEntry entry, DateTime timeStamp)
        {
            if (entry.Entity is IHasDeleteUser hasDeleteUser)
            {
                hasDeleteUser.DeleteDate = timeStamp;
                hasDeleteUser.DeletedUserId = UserId;
                entry.State = EntityState.Modified;
            }
        }
    }
}
