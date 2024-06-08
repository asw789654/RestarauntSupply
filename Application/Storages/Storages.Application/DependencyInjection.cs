using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Storages.Application.Caches;

namespace Storages.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddStoragesApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddSingleton<StoragesMemoryCache>()
            .AddSingleton<StoragesListMemoryCache>()
            .AddSingleton<StoragesCountMemoryCache>()
            .AddTransient<ICleanStoragesCacheService, CleanStoragesCacheService>();
    }
}