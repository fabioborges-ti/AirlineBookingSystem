using AirlineBookingSystem.Notifications.Core.Entities;
using AirlineBookingSystem.Notifications.Core.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace AirlineBookingSystem.Notifications.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly string _connectionString;

    private IDbConnection Connection => new SqlConnection(_connectionString);

    public NotificationRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task LogNotificationAsync(Notification notification)
    {
        using var conn = Connection;
        conn.Open();

        const string sql = @"
            INSERT INTO Notifications (Id, Recipient, Message, Type, SentAt)
            VALUES (@Id, @Recipient, @Message, @Type, @SentAt)";

        await conn.ExecuteAsync(sql, notification);
    }
}
