using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DepartmentMappings : Profile
    {
        public DepartmentMappings()
        {
            CreateMap<Department, DepartmentDto>();

            CreateMap<DepartmentDto, Department>();
        }
    }
}
