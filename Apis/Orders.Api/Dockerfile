#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Apis/Orders.Api/Orders.Api.csproj", "Apis/Orders.Api/"]
COPY ["Application/Core/Auth/Core.Auth.Api/Core.Auth.Api.csproj", "Application/Core/Auth/Core.Auth.Api/"]
COPY ["Application/Core/Auth/Core.Auth.Application/Core.Auth.Application.csproj", "Application/Core/Auth/Core.Auth.Application/"]
COPY ["Application/Core/Core.Users.Domain/Core.Users.Domain.csproj", "Application/Core/Core.Users.Domain/"]
COPY ["Application/Core/Core.Api/Core.Api.csproj", "Application/Core/Core.Api/"]
COPY ["Application/Core/Core.Application/Core.Application.csproj", "Application/Core/Core.Application/"]
COPY ["Application/Orders/Orders.Applications/Orders.Applications.csproj", "Application/Orders/Orders.Applications/"]
COPY ["Infrastracture.Mq/Infrastracture.Mq.csproj", "Infrastracture.Mq/"]
COPY ["Application/Orders/Orders.Domain/Orders.Domain.csproj", "Application/Orders/Orders.Domain/"]
COPY ["Application/Core/Core.Products.Domain/Core.Products.Domain.csproj", "Application/Core/Core.Products.Domain/"]
COPY ["Application/Core/Core.Storages.Domain/Core.Storages.Domain.csproj", "Application/Core/Core.Storages.Domain/"]
COPY ["Infrastructure/Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure/Infrastructure.Persistence/"]
COPY ["Application/Auth/Auth.Domain/Auth.Domain.csproj", "Application/Auth/Auth.Domain/"]
COPY ["Application/Storages/Storages.Domain/Storages.Domain.csproj", "Application/Storages/Storages.Domain/"]
RUN dotnet restore "./Apis/Orders.Api/Orders.Api.csproj"
COPY . .
WORKDIR "/src/Apis/Orders.Api"
RUN dotnet build "./Orders.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Orders.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orders.Api.dll"]