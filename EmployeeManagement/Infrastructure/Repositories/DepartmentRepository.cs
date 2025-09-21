using Domain.Entities;
using Domain.Interface.Repository;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : SoftDeletableGenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
