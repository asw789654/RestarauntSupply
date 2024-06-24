# ProductsSupply

## Introduction

Application for manage products in restaraunt by using modern approaches to development on the ASP.NET platform .

## Architecture design patterns used

- RESTful
- Command
- Command query responsibility segregation
- Mediator
- Onion
- Repository
- Chain of responsibility
- Write-through cache

## Libraries

- .NET 8 
- ASP.NET 8
- AutoMapper 13
- FluentValidation 11
- MediatR 12
- Serilog 3
- EntityFrameworkCore 8
- xunit 2.7
- NetArchTest.Rules 1.3

## Projects

### Application

#### Application/Core

- Core.Users.Domain - User and user roles entity
- Core.Application - Core business logic abstractions and realizations 
- Core.Api - Common middlewares and api services configuration
- Core.Auth.Application - Common auth business logic abstractions and realizations
- Core.Products.Domain - Product entity
- Core.Storages.Domain - Storage and Storage types entity

#### Application/Users

- Users.Application - Users management business logic

#### Application/Mails

- Mails.Domain - Mails entities
- Mails.Applications - Mails management business logic

#### Application/Auth

- Auth.Domain - Auth entities
- Auth.Application - Auth business logic

#### Application/Orders

- Orders.Domain - Orders entities
- Orders.Application - Orders business logic

#### Application/Products

- Products.Application - Products business logic

#### Application/Storages

- Storages.Application - Storages business logic

### Infrastructure

- Infrastructure.Persistence - Database connection realizations

### Apis

- Users.Api - Users management API
- Storages.Api - Storages management API
- Products.Api - Products management API
- Orders.Api - Orders management API
- Mails.Api - Mails management API
- Auth.Api - Auth API

### Tests

- Products.UnitTests - Unit test for Todos.Applications
- Users.UnitTests - Unit test for Users.Applications
- Auth.UnitTests  - Unit test for Auth.Applications

#### Tests/Core

- Core.ArchitectureTests - Architecture tests
- Core.Tests - Common test utils

##  Configurations

### Users, Products, Orders, Mails, Storages

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=RestarauntDb;Username=postgres;Password=789654",
    "Redis": "localhost:6379"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "LogsFolder": "Logs/"
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "sdversdfregsgdsgfsdfwesdfsdfsdfsf",
    "Issuer": "Products",
    "Audience": "Products"
  }
}
```

### Auth.Api

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=RestarauntDb;Username=postgres;Password=789654",
    "Redis": "localhost:6379"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "LogsFolder": "Logs/"
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "sdversdfregsgdsgfsdfwesdfsdfsdfsf",
    "Issuer": "Products",
    "Audience": "Products"
  },
  "TokensLifeTime": {
    "JwtToken" : 300,
    "RefreshToken" : 72000
  }
}
```

## How to run

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (>=8.0.3)
2. Install and run [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 
3. Insert **connection string** to database to **appsettings.json** files of apis projects. Example: 
```"DefaultConnection": "Server=.;database=Todos;Integrated Security=False;User Id=sa;Password=sqlServerPassword;MultipleActiveResultSets=True;Trust Server Certificate=true;"```
4. Insert logs folder path to **appsettings.json** files of apis projects. Example: ```"LogsFolder": "Logs/"```
5. Open target API project in terminal
6. Run command ```dotnet run```





