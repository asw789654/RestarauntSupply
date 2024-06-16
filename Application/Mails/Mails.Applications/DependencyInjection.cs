using Core.Application.Abstractions;
using FluentValidation;
using Infrastracture.Mq;
using Mails.Applications.Caches;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mails.Applications;

public static class DependencyInjection
{
    public static IServiceCollection AddMailsApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddSingleton<MailsMemoryCache>()
            .AddSingleton<MailsListMemoryCache>()
            .AddSingleton<MailsCountMemoryCache>()
            .AddTransient<ICleanMailsCacheService, CleanMailsCacheService>();
    }
}