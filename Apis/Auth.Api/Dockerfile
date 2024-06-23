#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Development
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Apis/Auth.Api/Auth.Api.csproj", "Apis/Auth.Api/"]
COPY ["Application/Auth/Auth.Application/Auth.Application.csproj", "Application/Auth/Auth.Application/"]
COPY ["Application/Core/Auth/Core.Auth.Application/Core.Auth.Application.csproj", "Application/Core/Auth/Core.Auth.Application/"]
COPY ["Application/Core/Core.Users.Domain/Core.Users.Domain.csproj", "Application/Core/Core.Users.Domain/"]
COPY ["Application/Core/Core.Application/Core.Application.csproj", "Application/Core/Core.Application/"]
COPY ["Application/Auth/Auth.Domain/Auth.Domain.csproj", "Application/Auth/Auth.Domain/"]
COPY ["Application/Core/Auth/Core.Auth.Api/Core.Auth.Api.csproj", "Application/Core/Auth/Core.Auth.Api/"]
COPY ["Application/Core/Core.Api/Core.Api.csproj", "Application/Core/Core.Api/"]
COPY ["Infrastructure/Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure/Infrastructure.Persistence/"]
COPY ["Application/Core/Core.Storages.Domain/Core.Storages.Domain.csproj", "Application/Core/Core.Storages.Domain/"]
COPY ["Application/Core/Core.Products.Domain/Core.Products.Domain.csproj", "Application/Core/Core.Products.Domain/"]
COPY ["Application/Orders/Orders.Domain/Orders.Domain.csproj", "Application/Orders/Orders.Domain/"]
RUN dotnet restore "./Apis/Auth.Api/Auth.Api.csproj"
COPY . .
WORKDIR "/src/Apis/Auth.Api"
RUN dotnet build "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]