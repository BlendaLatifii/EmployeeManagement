using Domain.Entities.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Abstraction
{
    public abstract class SoftDeletableConfiguration<T> : IEntityTypeConfiguration<T> where T : SoftDeletableEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasOne(x=> x.UpdateUser)
                .WithMany()
                .HasForeignKey(x => x.UpdateUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.DeletedUser)
               .WithMany()
               .HasForeignKey(x => x.DeletedUserId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CreateUser)
              .WithMany()
              .HasForeignKey(x => x.CreateUserId)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
