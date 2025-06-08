using AirlineBookingSystem.Flights.Application.Queries;
using AirlineBookingSystem.Flights.Core.Entities;
using AirlineBookingSystem.Flights.Core.Interfaces.Repositories;
using MediatR;

namespace AirlineBookingSystem.Flights.Application.Handlers;

public class GetAllFlightsHandler : IRequestHandler<GetAllFlightsQuery, IEnumerable<Flight>>
{
    private readonly IFlightRepository _repository;

    public GetAllFlightsHandler(IFlightRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Flight>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetFlightsAsync();

    }
}
