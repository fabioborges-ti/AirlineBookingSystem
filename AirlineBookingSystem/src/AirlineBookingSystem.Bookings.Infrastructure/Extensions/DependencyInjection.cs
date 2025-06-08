using AirlineBookingSystem.Bookings.Application.Consumers;
using AirlineBookingSystem.Bookings.Application.Handlers;
using AirlineBookingSystem.Bookings.Core.Interfaces.Repositories;
using AirlineBookingSystem.Bookings.Infrastructure.Repositories;
using AirlineBookingSystem.BuildingBlocks.Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

namespace AirlineBookingSystem.Bookings.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["CacheSettings:ConnectionString"]!;

        var redis = ConnectionMultiplexer.Connect(connectionString);

        services.AddSingleton<IConnectionMultiplexer>(redis);

        return services;
    }

    public static IServiceCollection RegisterMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            // Mark this as consumer 
            x.AddConsumer<NotificationEventConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["EventBusSettings:HostAddress"]);
                cfg.ReceiveEndpoint(EventBusConstant.NotificationSentQueue, c =>
                {
                    c.ConfigureConsumer<NotificationEventConsumer>(context);
                });
            });
        });

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IBookingRepository, BookingRepository>();

        return services;
    }

    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        var assemblies = new Assembly[]
        {
            Assembly.GetExecutingAssembly(),
            typeof(CreateBookingHandler).Assembly,
            typeof(GetBookingHandler).Assembly
        };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        return services;
    }
}
