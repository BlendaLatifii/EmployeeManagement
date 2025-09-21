using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IAuditData _auditData;
        public AppDbContext(DbContextOptions<AppDbContext> options, IAuditData auditData) : base(options)
        {
            _auditData = auditData; 
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public override int SaveChanges()
        {
            _auditData.Audit(ChangeTracker.Entries());
           return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            _auditData.Audit(ChangeTracker.Entries());
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
