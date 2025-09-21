using Domain.Entities;


namespace Domain.Interface.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {

        public Task<List<User>> GetUsersWithRole(CancellationToken cancellationToken);
        Task<User> GetUserWithRole(Guid id, CancellationToken cancellationToken);
    }
}
