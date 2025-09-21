using Application.Dtos.Department;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DepartmentMappings : Profile
    {
        public DepartmentMappings()
        {
            CreateMap<Department, DepartmentDetailDto>();

            CreateMap<AddDepartmentDto, Department>();
        }
    }
}
