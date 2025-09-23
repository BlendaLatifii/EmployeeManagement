using Application.Dtos.Employee;
using Application.Dtos.Users;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class EmployeeMappings : Profile
    {
        public EmployeeMappings()
        {

            CreateMap<Employee, EmployeeDetailDto>()
                .ForMember(e => e.Email, u => u.MapFrom(u => u.User.Email))
                .ForMember(e => e.LastName, u => u.MapFrom(u => u.User.LastName))
                .ForMember(e => e.Name, u => u.MapFrom(u => u.User.UserName))
                .ForMember(e => e.PhoneNumber, u => u.MapFrom(u => u.User.PhoneNumber));

            CreateMap<AddEmployeeDto, Employee>()
               .ForMember(x => x.CreateDate, y => y.Ignore())
               .ForMember(x => x.CreateUserId, y => y.Ignore())
               .ForMember(x => x.CreateUserId, y => y.Ignore())
               .ForMember(x => x.DeleteDate, y => y.Ignore())
               .ForMember(x => x.DeletedUserId, y => y.Ignore());

            CreateMap<AddEmployeeDto, SaveUserDto>()
                .ForMember(e => e.UserName, y => y.MapFrom(y => y.Name));

            CreateMap<EmployeeDetailDto, UserDetailsDto>()
              .ForMember(e => e.UserName, y => y.MapFrom(y => y.Name))
              .ForMember(e => e.Roles, y => y.Ignore())
              .ForMember(e => e.Id, y => y.Ignore());

        }
    }
}
