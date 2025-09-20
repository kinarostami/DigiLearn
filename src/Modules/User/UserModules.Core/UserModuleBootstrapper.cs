using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModules.Data;

namespace TIcketModules;

public static class UserModuleBootstrapper
{
    public static IServiceCollection InitUsertModule(this IServiceCollection service,IConfiguration config)
    {
        service.AddDbContext<UserContext>(optoin =>
        {
            optoin.UseSqlServer(config.GetConnectionString("User_Context"));
        });
        service.AddScoped<IUserService, UserService>();
        service.AddAutoMapper(typeof(MapperProfile).Assembly);
        return service;
    }
}
