using Core.Application.Abstractions;
using FluentValidation;
using Infrastracture.Mq;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Caches;
using System.Reflection;

namespace Orders.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddTransient<ICleanOrdersCacheService, CleanOrdersCacheService>()
            .AddTransient<IMqService, MqService>();
    }
}