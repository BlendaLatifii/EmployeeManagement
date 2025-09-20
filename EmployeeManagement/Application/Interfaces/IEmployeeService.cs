using Application.Dtos;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<EmployeeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<EmployeeDto> AddAsync(EmployeeDto model, CancellationToken cancellationToken);
        Task<EmployeeDto> UpdateAsync(EmployeeDto model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<int> CountAllEmployeesAsync(CancellationToken cancellationToken);
        Task<int> CountEmployeesInDepartmentAsync(string nameOfDepartment, CancellationToken cancellationToken);
        Task<int> GetEmployeeCountByJoiningDateAsync(int daysAgo, CancellationToken cancellationToken);
    }
}
