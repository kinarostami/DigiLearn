using AngleSharp;
using CoreModule.Application._Utils;
using CoreModule.Application.Category;
using CoreModule.Application.Category.Create;
using CoreModule.Application.Course;
using CoreModule.Application.Order;
using CoreModule.Application.Teacher;
using CoreModule.Domain.Category.DomainServices;
using CoreModule.Domain.Course.DomainServices;
using CoreModule.Domain.Order.DomainServices;
using CoreModule.Domain.Teacher.DomainServices;
using CoreModule.Facade;
using CoreModule.Infrastucture;
using CoreModule.Query;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace CoreModule.Config;

public static class CoreModuleBootstrapper
{
    public static IServiceCollection InitCoreMoudle(this IServiceCollection services, IConfiguration configuration)
    {
        CoreModuleFacadeBootstrapper.RegisterDependency(services);
        CoreModuleInfrastuctureBootstrapper.RegisterDependency(services, configuration);
        CoreModuleQueryBootstrapper.RegisterDependency(services, configuration);

        services.AddScoped<ICategoryDomainService, CategoryDomainService>();
        services.AddScoped<ICourseDomainService, CourseDomianService>();
        services.AddScoped<ITeacherDomainService, TeacherDomianService>();
        services.AddScoped<IOrderDomainService, OrderDomainService>();

        services.AddMediatR(typeof(CreateCourseCategoryCommand).Assembly);
        services.AddValidatorsFromAssembly(typeof(CreateCourseCategoryCommand).Assembly);
        return services;
    }
}
