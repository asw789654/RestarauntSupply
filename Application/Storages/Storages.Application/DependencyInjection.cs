using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Storages.Application.Caches;
using System.Reflection;

namespace Storages.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddStoragesApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddTransient<ICleanStoragesCacheService, CleanStoragesCacheService>();
    }
}