using System.Reflection;
using Core.Application.Abstractions;
using FluentValidation;
using Infrastracture.Mq;
using Microsoft.Extensions.DependencyInjection;
using Orders.Applications.Caches;

namespace Orders.Applications;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddSingleton<OrdersMemoryCache>()
            .AddSingleton<OrdersListMemoryCache>()
            .AddSingleton<OrdersCountMemoryCache>()
            .AddTransient<ICleanOrdersCacheService, CleanOrdersCacheService>()
            .AddTransient<IMqService,MqService>();        
    }
}