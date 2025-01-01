using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Unicode;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Taskly_Api.Common;
using Taskly_Api.Common.Errors;
using Taskly_Api.MapsterConfigs;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;
using Taskly_Domain.Other;
using Taskly_Infrastructure.Common.Persistence;
using Taskly_Infrastructure.Repositories;
using Taskly_Infrastructure.Services;

namespace Taskly_Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, TasklyProblemDetailsFactory>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IAvatarRepository, AvatarRepository>();
        services.AddHostedService<VerificationEmailCleaner>();
        services.AddSwagger();
        services.AddMappings();
        services.AddJWT(configuration);
        services.AddJWTToSwagger();
        services.AddIdentity();
        services.Configuring(configuration);

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Dashboard API", Version = "v1" });
        });

        return services; 
    }
   
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly()); 

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        AuthenticateMapsterConfig.Config();

        return services;
    }
    public static IServiceCollection AddJWT(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAuthentication(conf =>
        {
            conf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            conf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            conf.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(conf =>
        {
            conf.RequireHttpsMetadata = true;
            conf.SaveToken = true;
            conf.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["AuthenticationSettings:JwtKey"]!)
                    ),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });
        return services;
    }
    public static IServiceCollection AddJWTToSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options => 
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
            }
            });
        });
        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<UserEntity, IdentityRole<Guid>>(conf =>
        {
            conf.Password.RequiredLength = 10;
            conf.Password.RequireDigit = false;
            conf.Password.RequireLowercase = false;
            conf.Password.RequireUppercase = false;
            conf.Password.RequireNonAlphanumeric = false;

            conf.User.RequireUniqueEmail = true;

            conf.SignIn.RequireConfirmedEmail = true;

            conf.Lockout.MaxFailedAccessAttempts = 5;
            conf.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        })
        .AddEntityFrameworkStores<TasklyDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
    private static IServiceCollection Configuring(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthanticationSettings>(configuration.GetSection("AuthenticationSettings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        return services;
    }
}