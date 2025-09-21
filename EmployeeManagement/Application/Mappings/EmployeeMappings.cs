using Application.Dtos.Employee;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class EmployeeMappings : Profile
    {
        public EmployeeMappings() 
        {

            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeDto, Employee>();

        }
    }
}
