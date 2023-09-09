using AutoMapper;
using Auth.Models;
using Auth.Models.Dtos;

namespace Auth.Profiles{
    public class AuthProfile:Profile{
        public AuthProfile()
        {
            CreateMap<RegisterRequestDto, ApplicationUser>()
            .ForMember(dest=>dest.UserName, u=>u.MapFrom(reg=>reg.Email));

            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}