using Domain.Entities;
using Domain.Interface.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : SoftDeletableGenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public override async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await _dbSet.Where(employee => !employee.DeleteDate.HasValue).Include(employee => employee.User).ToListAsync(cancellationToken);
            return employees;
        }

        public override async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _dbSet.Where(employee => !employee.DeleteDate.HasValue).Include(employee => employee.User).FirstAsync(cancellationToken);

            return employee;
        }
        public async Task<int> CountAllEmployeesAsync(CancellationToken cancellationToken)
        {
            return await _context.Employees.Where(employee => !employee.DeleteDate.HasValue).CountAsync(cancellationToken);
        }

        public async Task<int> CountEmployeesInDepartmentAsync(String nameOfDepartment, CancellationToken cancellationToken)
        {
            return await _context.Employees.Where(employee => !employee.DeleteDate.HasValue)
                .Where(e => e.Department.Name == nameOfDepartment).CountAsync(cancellationToken);

        }

        public async Task<int> GetEmployeeCountByJoiningDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            return await _context.Employees.Where(employee => !employee.DeleteDate.HasValue).Where(e => e.DateOfJoining.Date >= date.Date).CountAsync(cancellationToken);
        }
    }
}
