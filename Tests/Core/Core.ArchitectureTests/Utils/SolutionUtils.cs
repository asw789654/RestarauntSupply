using System.Reflection;
using UsersApiClass = Users.Api.Apis.UsersApi;
using UsersApplicationDependencyInjectionClass = Users.Application.DependencyInjection;
using AuthApiClass = Auth.Api.Apis.AuthApi;
using Products.Applications;
using Products.Api.Apis;

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

            #region Auth

            typeof(Auth.Application.DependencyInjection).Assembly,
            typeof(AuthApiClass).Assembly,

            #endregion

            #endregion
        };
    }
}