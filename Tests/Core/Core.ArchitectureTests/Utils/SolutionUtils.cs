using System.Reflection;
using UsersApiClass = Users.Api.Apis.UsersApi;
using MailsApiClass = Mails.Api.Apis.MailsApi;
using StoragesApiClass = Storages.Api.Apis.StoragesApi;
using OrdersApiClass = Orders.Api.Apis.OrdersApi;
using UsersApplicationDependencyInjectionClass = Users.Application.DependencyInjection;
using StoragesApplicationDependencyInjectionClass = Storages.Application.DependencyInjection;
using OrdersApplicationDependencyInjectionClass = Orders.Application.DependencyInjection;
using MailsApplicationDependencyInjectionClass = Mails.Application.DependencyInjection;
using AuthApiClass = Auth.Api.Apis.AuthApi;
using Products.Api.Apis;
using Core.Storages.Domain;
using Products.Application;

namespace Core.ArchitectureTests.Utils;

public abstract class SolutionUtils
{
    public static Assembly[] GetSolutionAssemblies()
    {
        return new[]
        {
            #region Infrastructure

            typeof(Infrastructure.Persistence.DependencyInjection).Assembly,       

            #endregion

            #region Core

            typeof(Core.Users.Domain.ApplicationUser).Assembly,
            typeof(Core.Application.DependencyInjection).Assembly,
            typeof(Core.Api.DependencyInjection).Assembly,
            
            typeof(Core.Auth.Application.DependencyInjection).Assembly,
            typeof(Core.Auth.Api.DependencyInjection).Assembly,
            
            #endregion

            #region Applications
            
            #region Users
            
            typeof(UsersApplicationDependencyInjectionClass).Assembly,
            typeof(UsersApiClass).Assembly,
            
            #endregion

            #region Products

            typeof(DependencyInjection).Assembly,
            typeof(ProductsApi).Assembly,

            #endregion

            #region Storages

            typeof(StoragesApplicationDependencyInjectionClass).Assembly,
            typeof(StoragesApiClass).Assembly,

            #endregion

            #region Orders

            typeof(OrdersApplicationDependencyInjectionClass).Assembly,
            typeof(OrdersApiClass).Assembly,

            #endregion

            #region Mails

            typeof(MailsApplicationDependencyInjectionClass).Assembly,
            typeof(MailsApiClass).Assembly,

            #endregion

            #region Auth

            typeof(Auth.Application.DependencyInjection).Assembly,
            typeof(AuthApiClass).Assembly,

            #endregion

            #endregion
        };
    }
}