using BlogModules.Context;
using BlogModules.Repository.Categories;
using BlogModules.Repository.Posts;
using BlogModules.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogModules;

public static class BlogBootstrapper
{
    public static IServiceCollection InitBlogModule(this IServiceCollection service,IConfiguration config)
    {
        service.AddDbContext<BlogContext>(optoin =>
        {
            optoin.UseSqlServer(config.GetConnectionString(""));
        });
        service.AddScoped<ICategoryRepository, CategoryRepository>();
        service.AddScoped<IPostRepository, PostRepository>();
        service.AddScoped<IBlogService, BlogService>();
        //service.AddAutoMapper(typeof(MapperProfile).Assembly);
        return service;
    }
}
