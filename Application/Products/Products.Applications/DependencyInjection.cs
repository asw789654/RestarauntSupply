using Core.Application.Abstractions;
using FluentValidation;
using Infrastracture.Mq;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Caches;
using System.Reflection;

namespace Products.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddProductsApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddTransient<ICleanProductsCacheService, CleanProductsCacheService>()
            .AddTransient<IMqService, MqService>();
    }
}