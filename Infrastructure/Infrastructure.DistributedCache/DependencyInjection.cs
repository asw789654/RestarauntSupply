using Infrastructure.DistributedCache.Mails;
using Infrastructure.DistributedCache.Orders;
using Infrastructure.DistributedCache.Products;
using Infrastructure.DistributedCache.Storages;
using Infrastructure.DistributedCache.Users;
using Mails.Application.Caches;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Caches;
using Products.Application.Caches;
using Storages.Application.Caches;
using Users.Application.Caches;

namespace Infrastructure.DistributedCache;

public static class DependencyInjection
{
    public static IServiceCollection AddDistributedCacheServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        })

        .AddTransient<IProductsMemoryCache, ProductsMemoryCache>()
        .AddTransient<IProductsListMemoryCache, ProductsListMemoryCache>()
        .AddTransient<IProductsCountMemoryCache, ProductsCountMemoryCache>()

        .AddTransient<IMailsMemoryCache, MailsMemoryCache>()
        .AddTransient<IMailsListMemoryCache, MailsListMemoryCache>()
        .AddTransient<IMailsCountMemoryCache, MailsCountMemoryCache>()

        .AddTransient<IOrdersMemoryCache, OrdersMemoryCache>()
        .AddTransient<IOrdersListMemoryCache, OrdersListMemoryCache>()
        .AddTransient<IOrdersCountMemoryCache, OrdersCountMemoryCache>()

        .AddTransient<IApplicationUsersMemoryCache, ApplicationUsersMemoryCache>()
        .AddTransient<IApplicationUsersListMemoryCache, ApplicationUsersListMemoryCache>()
        .AddTransient<IApplicationUsersCountMemoryCache, ApplicationUsersCountMemoryCache>()
        .AddTransient<IApplicationUsersMailListMemoryCache, ApplicationUsersMailListMemoryCache>()

        .AddTransient<IStoragesMemoryCache, StoragesMemoryCache>()
        .AddTransient<IStoragesListMemoryCache, StoragesListMemoryCache>()
        .AddTransient<IStoragesCountMemoryCache, StoragesCountMemoryCache>()

        .AddTransient<RedisService>();
    }
}