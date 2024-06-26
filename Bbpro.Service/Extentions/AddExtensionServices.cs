﻿using Bbpro.Domain.Entities.Categories;
using Bbpro.Domain.Entities.Latests;
using Bbpro.Domain.Entities.Solutions;
using Bbpro.Service.Interfaces.Abouts;
using Bbpro.Service.Interfaces.Auths;
using Bbpro.Service.Interfaces.Brands;
using Bbpro.Service.Interfaces.Categories;
using Bbpro.Service.Interfaces.Contacts;
using Bbpro.Service.Interfaces.Latests;
using Bbpro.Service.Interfaces.Orders;
using Bbpro.Service.Interfaces.Projects;
using Bbpro.Service.Interfaces.Solutions;
using Bbpro.Service.Interfaces.Tokens;
using Bbpro.Service.Interfaces.Users;
using Bbpro.Service.Services.Abouts;
using Bbpro.Service.Services.Auths;
using Bbpro.Service.Services.Brands;
using Bbpro.Service.Services.Categories;
using Bbpro.Service.Services.Contacts;
using Bbpro.Service.Services.Latests;
using Bbpro.Service.Services.Orders;
using Bbpro.Service.Services.Projects;
using Bbpro.Service.Services.Solutions;
using Bbpro.Service.Services.Tokens;
using Bbpro.Service.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Bbpro.Service.Extentions;
public static class AddExtensionServices
{
    public static IServiceCollection AddServiceConfig(
    this IServiceCollection services
)
    {
        services.AddScoped<IAuthRepository, AuthService>();
        services.AddScoped<ITokenRepository, TokenService>();
        services.AddScoped<IUserRepository, UserService>();

        services.AddScoped<ICategoryRepository, CategoryService>();
        services.AddScoped<ISolutionRepository, SolutionService>();
        services.AddScoped<IProjectRepository, ProjectService>();
        services.AddScoped<ILatestRepository, LatestService>();
        services.AddScoped<IContactRepository, ContactService>();
        services.AddScoped<IAboutRepository, AboutService>();
        services.AddScoped<IBrandRepository, BrandService>();
        services.AddScoped<IOrderRepository, OrderService>();
        return services;
    }
}
