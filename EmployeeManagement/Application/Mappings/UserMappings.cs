using Application.Dtos;
using Application.Dtos.Users;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserDetailsDto>()
               .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
               .ForMember(x => x.UserName, y => y.MapFrom(x => x.UserName))
               .ForMember(x => x.Email, y => y.MapFrom(x => x.Email))
               .ForMember(x => x.LastName, y => y.MapFrom(x => x.LastName))
               .ForMember(x => x.PhoneNumber, y => y.MapFrom(x => x.PhoneNumber))
               .ForMember(x=> x.Roles,y => y.MapFrom(y=> y.Roles.Select(x=> x.Role.Name)));

            CreateMap<SaveUserDto, User>()
                .ForMember(x => x.UserName, y => y.MapFrom(x => x.UserName))
                .ForMember(x => x.Email, y => y.MapFrom(x => x.Email))
                .ForMember(x => x.LastName, y => y.MapFrom(x => x.LastName))
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(x => x.PhoneNumber));
        }
    }
}
