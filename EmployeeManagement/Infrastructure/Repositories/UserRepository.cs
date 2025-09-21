using Domain.Entities;
using Domain.Interface.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetUsersWithRole(CancellationToken cancellationToken)
        {
           return await _dbSet.Include(x=> x.Roles).ThenInclude(x=> x.Role).ToListAsync(cancellationToken);
        }

        public async Task<User> GetUserWithRole(Guid id,CancellationToken cancellationToken)
        {
            return await _dbSet.Where(x=> x.Id == id).Include(x => x.Roles).ThenInclude(x => x.Role).FirstAsync(cancellationToken);
        }
    }
}
