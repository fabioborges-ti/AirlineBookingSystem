using AirlineBookingSystem.BuildingBlocks.Common;
using AirlineBookingSystem.Payments.Application.Consumers;
using AirlineBookingSystem.Payments.Application.Handlers;
using AirlineBookingSystem.Payments.Core.Interfaces.Repositories;
using AirlineBookingSystem.Payments.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;
using System.Reflection;

namespace AirlineBookingSystem.Payments.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection");

        services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));

        return services;
    }

    public static IServiceCollection RegisterMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            // Mark this as consumer 
            x.AddConsumer<FlightBookedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
                cfg.ReceiveEndpoint(EventBusConstant.FlightBookedQueue, c =>
                {
                    c.ConfigureConsumer<FlightBookedConsumer>(context);
                });
            });
        });

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        return services;
    }

    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        var assemblies = new Assembly[]
        {
            Assembly.GetExecutingAssembly(),
            typeof(ProcessPaymentHandler).Assembly,
            typeof(RefundPaymentHandler).Assembly
        };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        return services;
    }
}
