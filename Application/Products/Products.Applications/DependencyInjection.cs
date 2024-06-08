using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Products.Applications.Caches;

namespace Products.Applications;

public static class DependencyInjection
{
    public static IServiceCollection AddProductsApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddSingleton<ProductsMemoryCache>()
            .AddSingleton<ProductsListMemoryCache>()
            .AddSingleton<ProductsCountMemoryCache>()
            .AddTransient<ICleanProductsCacheService, CleanProductsCacheService>();
    }
}