using CommentModules.Context;
using CommentModules.Services;
using CoreModule.Infrastucture.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommentModules;

public static class CommentBootstrapper
{
    public static IServiceCollection InitCommentModule(this IServiceCollection service,IConfiguration config)
    {
        service.AddDbContext<CommentContext>(optoin =>
        {
            optoin.UseSqlServer(config.GetConnectionString("Comment_Context"));
        });
        service.AddScoped<ICommentService,CommentService>();
        service.AddHostedService<UserRegisterEventHandler>();
        service.AddHostedService<UserEditedEventHandlers>();
        service.AddAutoMapper(typeof(MapperProfile).Assembly);
        return service;
    }
}
