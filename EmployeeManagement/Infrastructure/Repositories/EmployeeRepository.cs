using Domain.Entities;
using Domain.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<int> CountAllEmployeesAsync(CancellationToken cancellationToken)
        {
            return await _context.Employees.CountAsync(cancellationToken);
        }

        public async Task<int> CountEmployeesInDepartmentAsync(String nameOfDepartment, CancellationToken cancellationToken)
        {
            return await _context.Employees.Where(e => e.Department.Name == nameOfDepartment).CountAsync(cancellationToken);

        }

        public async Task<int> GetEmployeeCountByJoiningDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            return await _context.Employees.Where(e => e.DateOfJoining.Date >= date.Date).CountAsync(cancellationToken);
        }
    }
}
