using FluentValidation;
using Mails.Application.Caches;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mails.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddMailsApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddTransient<ICleanMailsCacheService, CleanMailsCacheService>();
    }
}