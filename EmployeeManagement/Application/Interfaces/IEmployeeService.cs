using Application.Dtos.Employee;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDetailDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<EmployeeDetailDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<EmployeeDetailDto> AddAsync(AddEmployeeDto model, CancellationToken cancellationToken);
        Task<EmployeeDetailDto> UpdateAsync(EmployeeDetailDto model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<int> CountAllEmployeesAsync(CancellationToken cancellationToken);
        Task<int> CountEmployeesInDepartmentAsync(string nameOfDepartment, CancellationToken cancellationToken);
        Task<int> GetEmployeeCountByJoiningDateAsync(int daysAgo, CancellationToken cancellationToken);
        Task<List<EmployeeDetailDto>> FilterEmployeeByDepartmentAsync( Guid departmentId, CancellationToken cancellationToken);
    }
}
