using AutoMapper;
using BlogModules.Domain;
using BlogModules.Service.DTOs.Query;

namespace BlogModules;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, BlogCategoryDto>().ReverseMap();
    }
}
