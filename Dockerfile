# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["/src/Planet.Transfer.Api.StartUp/Planet.Transfer.Api.StartUp.csproj", "src/Planet.Transfer.Api.StartUp/"]
COPY ["/src/Planet.Transfer.Api.Infrastructure/Planet.Transfer.Api.Infrastructure.csproj", "src/Planet.Transfer.Api.Infrastructure/"]
COPY ["/src/Planet.Transfer.Api.Application/Planet.Transfer.Api.Application.csproj", "src/Planet.Transfer.Api.Application/"]
COPY ["/src/Planet.Transfer.Api.Domain/Planet.Transfer.Api.Domain.csproj", "src/Planet.Transfer.Api.Domain/"]
COPY ["/src/Planet.Transfer.Api.Web/Planet.Transfer.Api.Web.csproj", "src/Planet.Transfer.Api.Web/"]
COPY ["/lib/Common.Lib/Common.Lib.csproj", "lib/Common.Lib/"]
RUN dotnet restore "./src/Planet.Transfer.Api.StartUp/Planet.Transfer.Api.StartUp.csproj"
COPY . .
WORKDIR "./src/Planet.Transfer.Api.StartUp"
RUN dotnet build "./Planet.Transfer.Api.StartUp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Planet.Transfer.Api.StartUp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Planet.Transfer.Api.StartUp.dll"]