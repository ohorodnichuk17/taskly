using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Taskly_Api.Common;
using Taskly_Api.Common.Errors;
using Taskly_Api.MapsterConfigs;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain;
using Taskly_Domain.Entities;
using Taskly_Domain.ValueObjects;
using Taskly_Infrastructure;
using Taskly_Infrastructure.Common.Persistence;
using Taskly_Infrastructure.Repositories;
using Taskly_Infrastructure.Services;

namespace Taskly_Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });
        services.AddSingleton<ProblemDetailsFactory, TasklyProblemDetailsFactory>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAchievementRepository, AchievementRepository>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IAvatarRepository, AvatarRepository>();
        services.AddScoped<IBoardRepository, BoardRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<ITableItemsRepository, TableItemsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRuleEvaluatorService, RuleEvaluatorService>();
        services.AddScoped<IBadgeService, BadgeService>();
        services.AddScoped<ITableItemsRepository, TableItemsRepository>();
        services.AddScoped<IInviteRepository, InviteRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddHostedService<CustomCleaner<VerificationEmailEntity>>();
        services.AddHostedService<CustomCleaner<ChangePasswordKeyEntity>>();
        services.AddSwagger();
        services.AddMappings();
        services.AddJWT(configuration);
        services.AddJWTToSwagger();
        services.AddIdentity();
        services.Configuring(configuration);
        services.AddInfrastructureServices();
        services.AddCustomCors();
        services.AddGeminiClient();
        services.AddSignalR();

       

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


        return services;
    }
    public static IServiceCollection AddJWT(this IServiceCollection services,IConfiguration configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["AuthenticationSettings:JwtKey"]!)
                    ),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
            };
            options.Events = new JwtBearerEvents()
            {
                OnMessageReceived = context => // ���� �������� ����� �� ��������� ������
                {               
                    if (context.Request.Cookies.ContainsKey("X-JWT-Token"))
                    {
                        context.Token = context.Request.Cookies["X-JWT-Token"];
                    }

                    return Task.CompletedTask;
                }
            };
            

        });

        services.AddAuthorization(conf =>
        {
            conf.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
        });

        /*services.ConfigureApplicationCookie(conf =>
        {
            conf.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };

            conf.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            };
        });*/

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
    
    private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddTransient<UserService>();

        return services;
    }

    
    private static IServiceCollection Configuring(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthenticationSettings>(configuration.GetSection("AuthenticationSettings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<GeminiSettings>(configuration.GetSection("Gemini"));

        return services;
    }

    private static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowPolicy", policy =>
            {
<<<<<<< HEAD
                /*policy.WithOrigins("http://localhost:5173")
                    .AllowCredentials() // ����� �� ����-�� ������ ���
                    .AllowAnyMethod() // ����� �� ����-�� ������
                    .AllowAnyHeader();*/
                policy.WithOrigins("https://taskly-frontend-5bz1.onrender.com")
                    .AllowCredentials() // ����� �� ����-�� ������ ���
                    .AllowAnyMethod() // ����� �� ����-�� ������
                    .AllowAnyHeader(); //����� �� ����-�� �������� ���
                //policy.AllowAnyOrigin() // ����� �� ����-�� ������
=======

                // policy.WithOrigins("https://taskly-frontend-5bz1.onrender.com", "http://localhost:5173")
                //     .AllowCredentials() // ����� �� ����-�� ������ ���
                //     .AllowAnyMethod() // ����� �� ����-�� ������
                //     .AllowAnyHeader();//����� �� ����-�� �������� ���
                policy.AllowAnyOrigin(); // ����� �� ����-�� ������
>>>>>>> 5242f178150e41d2c5a1892c3e3edea6c41810bf
            });
        });
        return services;
    }

    private static IServiceCollection AddGeminiClient(this IServiceCollection services)
    {
        services.AddSingleton<IGeminiApiClient, GeminiApiClient>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<GeminiSettings>>().Value;
            return new GeminiApiClient(settings.ApiKey, settings.Url);
        });
        return services;
    }
}