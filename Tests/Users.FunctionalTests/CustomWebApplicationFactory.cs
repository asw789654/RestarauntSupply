using Core.Application.Abstractions.Persistence;
using Core.Tests;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Users.FunctionalTests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ApplicationDbContext>));

            services.Remove(dbContextDescriptor);

            services.AddScoped<IContextTransactionCreator, MocContextTransactionCreator>();

            services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=ProductsTestDb;Username=postgres;Password=789654");
            });
        });
        
        builder.UseEnvironment("Development");
    }
}