using CoreModule.Domain.Category.Repository;
using CoreModule.Domain.Course.Repository;
using CoreModule.Domain.Teacher.Repository;
using CoreModule.Infrastucture.Persistent;
using CoreModule.Infrastucture.Persistent.Category;
using CoreModule.Infrastucture.Persistent.Course;
using CoreModule.Infrastucture.Persistent.Teacher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Infrastucture;

public class CoreModuleInfrastuctureBootstrapper
{
    public static void RegisterDependency(IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();

        services.AddDbContext<CoreMoudelEfContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("CoreModule_Context"));
        });
    }
}
