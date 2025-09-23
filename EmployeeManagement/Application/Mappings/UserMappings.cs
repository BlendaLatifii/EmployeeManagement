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
               .ForMember(x => x.Roles, y => y.MapFrom(y => y.UserRoles.Select(x => x.Role.Name).ToList()));

            CreateMap<SaveUserDto, User>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UserName, y => y.MapFrom(x => x.UserName))
                .ForMember(x => x.Email, y => y.MapFrom(x => x.Email))
                .ForMember(x => x.LastName, y => y.MapFrom(x => x.LastName))
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.EmailConfirmed, y => y.Ignore())
                .ForMember(x => x.PhoneNumberConfirmed, y => y.Ignore())
                .ForMember(x => x.AccessFailedCount, y => y.Ignore())
                .ForMember(x => x.ConcurrencyStamp, y => y.Ignore())
                .ForMember(x => x.LockoutEnabled, y => y.Ignore())
                .ForMember(x => x.LockoutEnd, y => y.Ignore())
                .ForMember(x => x.NormalizedEmail, y => y.Ignore())
                .ForMember(x => x.NormalizedUserName, y => y.Ignore())
                .ForMember(x => x.PasswordHash, y => y.Ignore())
                .ForMember(x => x.PhoneNumberConfirmed, y => y.Ignore())
                .ForMember(x => x.UserRoles, y => y.Ignore())
                .ForMember(x => x.SecurityStamp, y => y.Ignore())
                .ForMember(x => x.TwoFactorEnabled, y => y.Ignore());

        }
    }
}
