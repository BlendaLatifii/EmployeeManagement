using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<DepartmentDto> _validator;

    public DepartmentService(IDepartmentRepository departmentRepository,IMapper mapper, IValidator<DepartmentDto> validator)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<List<DepartmentDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<DepartmentDto>>(departments);
    }
    public async Task<DepartmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);

        if (department is null)
        {
            throw new KeyNotFoundException();
        }

        return _mapper.Map<DepartmentDto>(department);
    }
    public async Task<DepartmentDto> AddAsync(DepartmentDto model, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(model);
        var department = _mapper.Map<Department>(model);

        await _departmentRepository.AddAsync(department, cancellationToken);

        return await GetByIdAsync(department.Id, cancellationToken);
    }

    public async Task<DepartmentDto> UpdateAsync(DepartmentDto model, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(model);

        var department = _mapper.Map<Department>(model);

        await _departmentRepository.UpdateAsync(department, cancellationToken);
        
        return _mapper.Map<DepartmentDto>(department);
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