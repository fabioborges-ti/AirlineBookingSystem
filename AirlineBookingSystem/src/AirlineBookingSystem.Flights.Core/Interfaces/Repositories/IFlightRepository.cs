using AirlineBookingSystem.Flights.Core.Entities;

namespace AirlineBookingSystem.Flights.Core.Interfaces.Repositories;

public interface IFlightRepository
{
    Task<IEnumerable<Flight>> GetFlightsAsync();
    Task AddFlightAsync(Flight flight);
    Task DeleteFlightAsync(Guid id);
}
