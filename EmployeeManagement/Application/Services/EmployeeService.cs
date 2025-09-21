using Application.Dtos.Employee;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<EmployeeDto> _validator;
    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IValidator<EmployeeDto> validator)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<List<EmployeeDetailDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<EmployeeDetailDto>>(employees);
    }
    public async Task<EmployeeDetailDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {

        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);

        if (employee == null)
        {
            throw new Exception();
        }

        return _mapper.Map<EmployeeDetailDto>(employee);
    }
    public async Task<EmployeeDetailDto> AddAsync(AddEmployeeDto model, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(model);

        var employee = _mapper.Map<Employee>(model);
        await _employeeRepository.AddAsync(employee, cancellationToken);
        return await  GetByIdAsync(employee.Id,cancellationToken);
    }

    public async Task<EmployeeDetailDto> UpdateAsync(EmployeeDetailDto model, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(model);

        var employee = _mapper.Map<Employee>(model);
        await _employeeRepository.UpdateAsync(employee, cancellationToken);
        return _mapper.Map<EmployeeDetailDto>(employee);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);

        if (employee == null)
        {
            throw new Exception("Employee not found!");
        }

        await _employeeRepository.DeleteAsync(employee, cancellationToken);
    }

    public async Task<int> CountAllEmployeesAsync(CancellationToken cancellationToken) =>
        await _employeeRepository.Query().CountAsync(cancellationToken);

    public async Task<int> CountEmployeesInDepartmentAsync(string nameOfDepartment, CancellationToken cancellationToken) =>
        await _employeeRepository.Query().Where(employe => employe.Department.Name == nameOfDepartment).CountAsync(cancellationToken);

    public async Task<int> GetEmployeeCountByJoiningDateAsync(int daysAgo, CancellationToken cancellationToken) =>
        await _employeeRepository.Query().Where(employe => employe.DateOfJoining.Date >= DateTime.Now.AddDays(-daysAgo)).CountAsync(cancellationToken);
}