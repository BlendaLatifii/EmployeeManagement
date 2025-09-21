using Domain.Entities.Abstraction;
using Domain.Interface.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SoftDeletableGenericRepository<TEntity> : GenericRepository<TEntity>, IGenericRepository<TEntity> where TEntity : SoftDeletableEntity
    {
        public SoftDeletableGenericRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.Where(x=> !x.DeleteDate.HasValue).ToListAsync(cancellationToken);
        }
    }
}
