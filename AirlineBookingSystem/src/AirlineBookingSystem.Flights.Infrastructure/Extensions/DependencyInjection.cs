using AirlineBookingSystem.Flights.Application.Handlers;
using AirlineBookingSystem.Flights.Core.Interfaces.Repositories;
using AirlineBookingSystem.Flights.Infrastructure.Data;
using AirlineBookingSystem.Flights.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AirlineBookingSystem.Flights.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFlightContext, FlightContext>();
        services.AddScoped<IFlightRepository, FlightRepository>();

        return services;
    }

    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        var assemblies = new Assembly[]
        {
            Assembly.GetExecutingAssembly(),
            typeof(CreateFlightHandler).Assembly,
            typeof(DeleteFlightHandler).Assembly,
            typeof(GetAllFlightsHandler).Assembly,
        };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        return services;
    }
}
