﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240608205943_ProductsChange")]
    partial class ProductsChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Auth.Domain.RefreshToken", b =>
                {
                    b.Property<Guid>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Expired")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Core.Products.Domain.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("SpoilTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("StorageId")
                        .HasColumnType("integer");

                    b.Property<int>("Volume")
                        .HasColumnType("integer");

                    b.HasKey("ProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("StorageId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Core.Storages.Domain.StorageType", b =>
                {
                    b.Property<int>("StorageTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StorageTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("StorageTypeId");

                    b.ToTable("StorageTypes");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUser", b =>
                {
                    b.Property<Guid>("ApplicationUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastSingInDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ApplicationUserId");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserApplicationUserRole", b =>
                {
                    b.Property<int>("ApplicationUserRoleId")
                        .HasColumnType("integer");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uuid");

                    b.HasKey("ApplicationUserRoleId", "ApplicationUserId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("ApplicationUserApplicationUserRole");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserRole", b =>
                {
                    b.Property<int>("ApplicationUserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ApplicationUserRoleId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("ApplicationUserRoleId");

                    b.ToTable("ApplicationUserRoles");
                });

            modelBuilder.Entity("Orders.Domain.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Orders.Domain.OrderStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("StatusId");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("Storages.Domain.Storage", b =>
                {
                    b.Property<int>("StorageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StorageId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("StorageTypeId")
                        .HasColumnType("integer");

                    b.HasKey("StorageId");

                    b.HasIndex("StorageTypeId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("Auth.Domain.RefreshToken", b =>
                {
                    b.HasOne("Core.Users.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("Core.Products.Domain.Product", b =>
                {
                    b.HasOne("Orders.Domain.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("Storages.Domain.Storage", null)
                        .WithMany("Products")
                        .HasForeignKey("StorageId");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserApplicationUserRole", b =>
                {
                    b.HasOne("Core.Users.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Users.Domain.ApplicationUserRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("ApplicationUserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Orders.Domain.Order", b =>
                {
                    b.HasOne("Orders.Domain.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Users.Domain.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("OrderStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Storages.Domain.Storage", b =>
                {
                    b.HasOne("Core.Storages.Domain.StorageType", "StorageType")
                        .WithMany()
                        .HasForeignKey("StorageTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StorageType");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUser", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Orders.Domain.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Storages.Domain.Storage", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
