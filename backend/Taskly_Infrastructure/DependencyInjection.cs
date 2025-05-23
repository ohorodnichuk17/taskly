using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       ConfigurationManager configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddPersistence(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        string connStr = configuration.GetConnectionString("DefaultConnection")!;

        services.AddDbContext<TasklyDbContext>(opt =>
        {
            opt.UseSqlServer(connStr);

            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });


        return services;
    }
}