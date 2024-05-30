using Bbpro.Api.ActionFilters;
using Bbpro.Domain.Entities.Latests;
using Bbpro.Domain.Entities.MainContact;
using Bbpro.Domain.Entities.Projects;
using Bbpro.Domain.Entities.Solutions;
using Bbpro.Domain.Entities.Users;
using Bbpro.Domain.Interface;
using Bbpro.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Bbpro.Api.Configurations;

public static class ExtentionFunctions
{

    public static IServiceCollection AddServiceFunctionsConfiguration(
       this IServiceCollection services
   )
    {
        services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();

        services.AddScoped<IGenericRepository<Solution>, GenericRepository<Solution>>();
        services.AddScoped<IGenericRepository<Project>, GenericRepository<Project>>();
        services.AddScoped<IGenericRepository<Latest>, GenericRepository<Latest>>();
        services.AddScoped<IGenericRepository<Contact>, GenericRepository<Contact>>();

        return services;
    }
    public static IServiceCollection AddErrorFilter(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ValidationActionFilter));
        });
        services.AddScoped<ValidationActionFilter>();
        return services;
    }
    public static IServiceCollection AddImageSizeMax(this IServiceCollection services)
    {
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 5368709120;
        });
        services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = 5368709120;
        });
        services.Configure<IISServerOptions>(options =>
        {
            options.MaxRequestBodySize = 5368709120;
        });
        return services;
    }


    //JWT bearer extention function
    public static void AddSwaggerService(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = configuration.GetSection("Jwt");
        services.AddSwaggerGen(p =>
        {
            p.ResolveConflictingActions(ad => ad.First());
            p.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                }
            );
            p.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                }
            );
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])
                    )
                };
            });
    }
}
