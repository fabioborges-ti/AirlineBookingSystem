using MediatR;

namespace AirlineBookingSystem.Bookings.Application.Commands;

public record CreateBookingCommand(Guid FightId, string PassengerName, string SeatNumber) : IRequest<Guid>;
