using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionModule.Context;
using TransactionModule.Services;
using TransactionModule.Services.ZarinPal;

namespace TransactionModule;

public static class TransactionBootstrapper
{
    public static IServiceCollection InitTransactionModule(this IServiceCollection service, IConfiguration config)
    {
        service.AddDbContext<TransactionContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("transaction_Context"));
        });
        service.AddTransient<IUserTransactionsService, UserTransactionsService>();
        //service.AddTransient<IZarinPalService, ZarinPalService>();
        return service;
    }
}
