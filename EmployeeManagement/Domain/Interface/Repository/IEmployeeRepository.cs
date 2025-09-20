using Domain.Entities;

namespace Domain.Interface.Repository
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        Task<int> CountAllEmployeesAsync(CancellationToken cancellationToken);

       Task<int> CountEmployeesInDepartmentAsync(string nameOfDepartment, CancellationToken cancellationToken);

        Task<int> GetEmployeeCountByJoiningDateAsync(DateTime date, CancellationToken cancellationToken);
    }
}
