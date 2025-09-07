# ==========================
# Етап 1: build
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Planet.Transfer.Api.StartUp/Planet.Transfer.Api.StartUp.csproj", "src/Planet.Transfer.Api.StartUp/"]
COPY ["src/Planet.Transfer.Api.Infrastructure/Planet.Transfer.Api.Infrastructure.csproj", "src/Planet.Transfer.Api.Infrastructure/"]
COPY ["src/Planet.Transfer.Api.Application/Planet.Transfer.Api.Application.csproj", "src/Planet.Transfer.Api.Application/"]
COPY ["src/Planet.Transfer.Api.Domain/Planet.Transfer.Api.Domain.csproj", "src/Planet.Transfer.Api.Domain/"]
COPY ["lib/Common.Lib/Common.Lib.csproj", "lib/Common.Lib/"]
COPY ["src/Planet.Transfer.Api.Web/Planet.Transfer.Api.Web.csproj", "src/Planet.Transfer.Api.Web/"]
RUN dotnet restore "./src/Planet.Transfer.Api.StartUp/Planet.Transfer.Api.StartUp.csproj"
COPY . .
WORKDIR "/src/src/Planet.Transfer.Api.StartUp"
RUN dotnet build "./Planet.Transfer.Api.StartUp.csproj" -c $BUILD_CONFIGURATION -o /app/build


# ==========================
# Етап 2: publish
# ==========================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Planet.Transfer.Api.StartUp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ==========================
# Етап 3: runtime
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .



# Render слуша на порт 10000


ENTRYPOINT ["dotnet", "Planet.Transfer.Api.StartUp.dll"]
