using CoreModue.Facade.Category;
using CoreModue.Facade.Course;
using CoreModue.Facade.Teacher;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Facade;

public class CoreModuleFacadeBootstrapper
{
    public static void RegisterDependency(IServiceCollection services)
    {
        services.AddScoped<ITeacherFacade, TeacherFacade>();
        services.AddScoped<ICourseCategoryFacade, CourseCategoryFacade>();
        services.AddScoped<ICourseFacade, CourseFacade>();
    }
}
