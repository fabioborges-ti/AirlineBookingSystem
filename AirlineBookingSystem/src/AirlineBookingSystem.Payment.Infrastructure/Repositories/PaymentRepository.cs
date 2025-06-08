using AirlineBookingSystem.Payments.Core.Entities;
using AirlineBookingSystem.Payments.Core.Interfaces.Repositories;
using Dapper;
using System.Data;

namespace AirlineBookingSystem.Payments.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly IDbConnection _dbConnection;

    public PaymentRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task ProcessPaymentAync(Payment payment)
    {
        using var conn = _dbConnection;
        conn.Open();

        const string sql = @"
            INSERT INTO Payments (Id, BookingId, Amount, PaymentDate)
            VALUES (@Id, @BookingId, @Amount, @PaymentDate)";

        await conn.ExecuteAsync(sql, payment);
    }

    public async Task RefundPaymentAsync(Guid id)
    {
        using var conn = _dbConnection;
        conn.Open();

        const string sql = @"
            DELETE FROM Payments
            WHERE Id = @Id";

        await conn.ExecuteAsync(sql, new { Id = id });
    }
}
