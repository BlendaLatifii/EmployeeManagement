using Application.Dtos.Department;

namespace Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDetailDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<DepartmentDetailDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<DepartmentDetailDto> AddAsync(AddDepartmentDto model, CancellationToken cancellationToken);
        Task<DepartmentDetailDto> UpdateAsync(UpdateDepartmentDto model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<int> GetNumberOfDepartmentsAsync(CancellationToken cancellationToken);

    }
}
