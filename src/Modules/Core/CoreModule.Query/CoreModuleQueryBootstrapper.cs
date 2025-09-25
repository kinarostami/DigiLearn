using CoreModule.Domain.Category.Repository;
using CoreModule.Domain.Course.Repository;
using CoreModule.Domain.Teacher.Repository;
using CoreModule.Infrastucture.Persistent;
using CoreModule.Infrastucture.Persistent.Category;
using CoreModule.Infrastucture.Persistent.Course;
using CoreModule.Infrastucture.Persistent.Teacher;
using CoreModule.Query._Data;
using CoreModule.Query.Teacher.GetById;
using CoreModule.Query.Teacher.GetByUserId;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query;

public class CoreModuleQueryBootstrapper
{
    public static void RegisterDependency(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(QueryContext));
        services.AddMediatR(typeof(GetByUserIdTeacherQuery));

        services.AddDbContext<QueryContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("CoreModule_Context"));
        });
    }
}
