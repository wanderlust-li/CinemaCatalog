using System.Reflection;
using CinemaCatalog.Application.IService;
using CinemaCatalog.Application.Service;
using CinemaCatalog.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaCatalog.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IFilmCategoryService, FilmCategoryService>();
        services.AddScoped<IFilmService, FilmService>();
        
        return services;
    }
}