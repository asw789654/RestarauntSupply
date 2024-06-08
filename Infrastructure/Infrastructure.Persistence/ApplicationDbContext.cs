using Auth.Domain;
using Core.Products.Domain;
using Core.Storages.Domain;
using Core.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Orders.Domain;
using System.Reflection;

namespace Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    #region Users

    internal DbSet<ApplicationUser> ApplicationUsers { get; } = default!;

    internal DbSet<ApplicationUserRole> ApplicationUserRoles { get; } = default!;

    internal DbSet<ApplicationUserApplicationUserRole> ApplicationUserApplicationUserRole { get; } = default!;

    #endregion

    #region Auth

    internal DbSet<RefreshToken> RefreshTokens { get; } = default!;

    #endregion

    #region Products

    internal DbSet<Product> Products { get; } = default!;

    #endregion

    #region Storages

    internal DbSet<Storage> Storages { get; } = default!;

    internal DbSet<StorageType> StorageTypes { get; } = default!;

    #endregion

    #region Orders

    internal DbSet<Order> Orders { get; } = default!;

    internal DbSet<OrderStatus> OrderStatuses { get; } = default!;

    #endregion

    #region Ef

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    #endregion
}