using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TIcketModules.Core.Services;
using TIcketModules.Data;

namespace TIcketModules;

public static class TicketBootstrapper
{
    public static IServiceCollection InitTicketModule(this IServiceCollection service,IConfiguration config)
    {
        service.AddDbContext<TicketContext>(optoin =>
        {
            optoin.UseSqlServer(config.GetConnectionString("Ticket_Context"));
        });
        service.AddScoped<ITicketService, TicketService>();
        service.AddAutoMapper(typeof(TicketMapperProfile).Assembly);
        return service;
    }
}
