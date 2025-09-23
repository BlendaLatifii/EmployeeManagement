using Application.Dtos.Department;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DepartmentMappings : Profile
    {
        public DepartmentMappings()
        {
            CreateMap<Department, DepartmentDetailDto>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
               .ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
               .ForMember(x => x.Description, y => y.MapFrom(x => x.Description))
                .ForMember(x => x.Highlighted, y => y.Ignore());

            CreateMap<AddDepartmentDto, Department>()
             .ForMember(x => x.Name, y => y.MapFrom(y => y.Name))
             .ForMember(x => x.Description, y => y.MapFrom(y => y.Description))
             .ForMember(x => x.CreateDate, y => y.Ignore())
            .ForMember(x => x.CreateUserId, y => y.Ignore())
            .ForMember(x => x.DeleteDate, y => y.Ignore())
            .ForMember(x => x.DeletedUserId, y => y.Ignore());
        }
    }
}
