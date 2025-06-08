using AirlineBookingSystem.Bookings.Core.Entities;

namespace AirlineBookingSystem.Bookings.Core.Interfaces.Repositories;

public interface IBookingRepository
{
    Task AddBookingAsync(Booking booking);
    Task<Booking?> GetBookingByIdAsync(Guid id);
}
