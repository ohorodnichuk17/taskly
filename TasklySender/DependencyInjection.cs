using Mapster;
using MapsterMapper;
using System.Reflection;
using TasklySender_Application.Interfaces;
using TasklySender_Domain.Common;
using TasklySender_Infrastructure.Services;


namespace TasklySender;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailService, EmailService>();
        services.AddCors();
        services.AddMappings();

        return services;
    }
    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowPolicy",policy =>
            {
                policy.WithOrigins("http://localhost:5258")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });
        return services;
    }
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
   
}
