using Bbpro.Domain.Entities.Latests;
using Bbpro.Domain.Entities.Solutions;
using Bbpro.Service.Interfaces.Auths;
using Bbpro.Service.Interfaces.Contacts;
using Bbpro.Service.Interfaces.Latests;
using Bbpro.Service.Interfaces.Projects;
using Bbpro.Service.Interfaces.Solutions;
using Bbpro.Service.Interfaces.Tokens;
using Bbpro.Service.Interfaces.Users;
using Bbpro.Service.Services.Auths;
using Bbpro.Service.Services.Contacts;
using Bbpro.Service.Services.Latests;
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

        services.AddScoped<ISolutionRepository, SolutionService>();
        services.AddScoped<IProjectRepository, ProjectService>();
        services.AddScoped<ILatestRepository, LatestService>();
        services.AddScoped<IContactRepository, ContactService>();

        return services;
    }
}
