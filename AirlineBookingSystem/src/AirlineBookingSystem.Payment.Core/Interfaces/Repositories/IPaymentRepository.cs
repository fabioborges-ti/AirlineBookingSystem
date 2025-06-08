using AirlineBookingSystem.Payments.Core.Entities;

namespace AirlineBookingSystem.Payments.Core.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task ProcessPaymentAync(Payment payment);
    Task RefundPaymentAsync(Guid id);
}
