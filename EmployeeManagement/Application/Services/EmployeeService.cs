using Application.Dtos.Employee;
using Application.Dtos.Users;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interface.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserService _userService;
    private readonly DbInitializerConfig _dbInitializerConfig;
    private readonly IMapper _mapper;
    private readonly IValidator<EmployeeDto> _validator;
    public EmployeeService(IEmployeeRepository employeeRepository, IUserService userService, IMapper mapper, IValidator<EmployeeDto> validator,DbInitializerConfig config)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _validator = validator;
        _userService = userService;
        _dbInitializerConfig = config;
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
       await _validator.ValidateAndThrowAsync(model);

        var saveUserDto = _mapper.Map<SaveUserDto>(model);
        saveUserDto.Password = _dbInitializerConfig.Employee.Password;
        saveUserDto.ConfirmPassword = saveUserDto.Password;
        saveUserDto.Roles.Add(Domain.Enum.Role.Employee);

        var user = await _userService.AddAsync(saveUserDto, cancellationToken);

        var employee = _mapper.Map<Employee>(model);
        employee.UserId = user.Id;

        await _employeeRepository.AddAsync(employee, cancellationToken);
       
        return await  GetByIdAsync(employee.Id,cancellationToken);
    }

    public async Task<EmployeeDetailDto> UpdateAsync(EmployeeDetailDto model, CancellationToken cancellationToken)
    {
       await  _validator.ValidateAndThrowAsync(model);

        var updateUserDto = _mapper.Map<UserDetailsDto>(model);
        var existingEmployee = await _employeeRepository.GetByIdAsync(model.Id, cancellationToken);

        updateUserDto.Id = existingEmployee!.UserId;

        await _userService.UpdateUserAsync(updateUserDto, cancellationToken);

        existingEmployee.DepartmentId = model.DepartmentId;
        existingEmployee.DateOfJoining = model.DateOfJoining;

        await _employeeRepository.UpdateAsync(existingEmployee, cancellationToken);
        return _mapper.Map<EmployeeDetailDto>(existingEmployee);
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

    public async Task<List<EmployeeDetailDto>> FilterEmployeeByDepartmentAsync(Guid departmentId , CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.Query()
            .Include(employee => employee.User)
            .Where(employee => !employee.DeleteDate.HasValue)
            .Where(employee => employee.DepartmentId == departmentId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EmployeeDetailDto>>(employees);  
    }
       
    
}