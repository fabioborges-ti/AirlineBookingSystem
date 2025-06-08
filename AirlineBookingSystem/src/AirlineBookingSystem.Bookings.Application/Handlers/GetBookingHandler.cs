using AirlineBookingSystem.Bookings.Application.Queries;
using AirlineBookingSystem.Bookings.Core.Entities;
using AirlineBookingSystem.Bookings.Core.Interfaces.Repositories;
using MediatR;

namespace AirlineBookingSystem.Bookings.Application.Handlers;

public class GetBookingHandler : IRequestHandler<GetBookingQuery, Booking?>
{
    private readonly IBookingRepository _repository;

    public GetBookingHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<Booking?> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetBookingByIdAsync(request.Id);
    }
}
