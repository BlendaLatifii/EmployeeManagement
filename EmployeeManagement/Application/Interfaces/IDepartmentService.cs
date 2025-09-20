using Application.Dtos;

namespace Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<DepartmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<DepartmentDto> AddAsync(DepartmentDto model, CancellationToken cancellationToken);
        Task<DepartmentDto> UpdateAsync(DepartmentDto model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<int> GetNumberOfDepartmentsAsync(CancellationToken cancellationToken);

    }
}
