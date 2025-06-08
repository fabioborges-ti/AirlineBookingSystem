# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AirlineBookingSystem.Bookings.Api/AirlineBookingSystem.Bookings.Api.csproj", "src/AirlineBookingSystem.Bookings.Api/"]
COPY ["src/AirlineBookingSystem.Bookings.Application/AirlineBookingSystem.Bookings.Application.csproj", "src/AirlineBookingSystem.Bookings.Application/"]
COPY ["src/AirlineBookingSystem.Bookings.Core/AirlineBookingSystem.Bookings.Core.csproj", "src/AirlineBookingSystem.Bookings.Core/"]
COPY ["src/AirlineBookingSystem.BuildingBlocks/AirlineBookingSystem.BuildingBlocks.csproj", "src/AirlineBookingSystem.BuildingBlocks/"]
COPY ["src/AirlineBookingSystem.Bookings.Infrastructure/AirlineBookingSystem.Bookings.Infrastructure.csproj", "src/AirlineBookingSystem.Bookings.Infrastructure/"]
RUN dotnet restore "./src/AirlineBookingSystem.Bookings.Api/AirlineBookingSystem.Bookings.Api.csproj"
COPY . .
WORKDIR "/src/src/AirlineBookingSystem.Bookings.Api"
RUN dotnet build "./AirlineBookingSystem.Bookings.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AirlineBookingSystem.Bookings.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirlineBookingSystem.Bookings.Api.dll"]