using AutoMapper;
using BlogModules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.Core;
using UserModule.Core.Services;
using UserModules.Data;

namespace TIcketModules;

public static class UserModuleBootstrapper
{
    public static IServiceCollection InitUsertModule(this IServiceCollection service,IConfiguration config)
    {
        service.AddDbContext<UserContexts>(optoin =>
        {
            optoin.UseSqlServer(config.GetConnectionString("User_Contexts"));
        });
        //service.AddMediatR(typeof(UserModuleBootstrapper).Assembly);

        service.AddScoped<IUserFacade, UserFacade>();
        service.AddScoped<IRoleFacade, RoleFacade>();

        service.AddAutoMapper(typeof(UserModuleBootstrapper).Assembly);
        //service.AddValidatorsFromAssembly(typeof(UserModuleBootstrapper).Assembly);
        return service;
    }
}
