using AutoMapper;
using UserModule.Core.Queries._DTOs;
using UserModules.Data.Entities.Users;

namespace UserModule.Core;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
    }
}