using Application.Dtos.Department;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Repository;
using Domain.Interface.Security;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IAuthorizationManager _authorizationManager;
    private readonly IMapper _mapper;
    private readonly IValidator<DepartmentDto> _validator;

    public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IAuthorizationManager authorizationManager, IValidator<DepartmentDto> validator)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _validator = validator;
        _authorizationManager = authorizationManager;
    }
    public async Task<List<DepartmentDetailDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAllAsync(cancellationToken);

        var departmentDtos = _mapper.Map<List<DepartmentDetailDto>>(departments);

        var departmentId = _authorizationManager.GetDepartmentId();
        if (departmentId.HasValue)
        {
            var department = departmentDtos.FirstOrDefault(x => x.Id == departmentId.Value);
            if (department != null)
            {
                department.Highlighted = true;
            }
        }
        return departmentDtos;

    }
    public async Task<DepartmentDetailDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);

        if (department is null)
        {
            throw new KeyNotFoundException();
        }

        return _mapper.Map<DepartmentDetailDto>(department);
    }
    public async Task<DepartmentDetailDto> AddAsync(AddDepartmentDto model, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(model);

        var department = _mapper.Map<Department>(model);

        await _departmentRepository.AddAsync(department, cancellationToken);

        return await GetByIdAsync(department.Id, cancellationToken);
    }

    public async Task<DepartmentDetailDto> UpdateAsync(UpdateDepartmentDto model, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(model);
        var existing = await _departmentRepository.GetByIdAsync(model.Id, cancellationToken);

        existing.Name = model.Name;
        existing.Description = model.Description;


        await _departmentRepository.UpdateAsync(existing, cancellationToken);

        return _mapper.Map<DepartmentDetailDto>(existing);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);

        if (department == null)
        {
            throw new KeyNotFoundException("Department not found!");
        }

        await _departmentRepository.DeleteAsync(department, cancellationToken);
    }

    public async Task<int> GetNumberOfDepartmentsAsync(CancellationToken cancellationToken)
       => await _departmentRepository.Query().CountAsync(cancellationToken);
}