using CinemaCatalog.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaCatalog.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CinemaCatalogContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
       
        return services;
    }
}