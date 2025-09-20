using AutoMapper;
using BlogModules.Domain;
using BlogModules.Service.DTOs.Command;
using BlogModules.Service.DTOs.Query;

namespace BlogModules;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, BlogCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Post, BlogPostDto>().ReverseMap();
        CreateMap<Post, CreatePostCommand>().ReverseMap();
    }
}
