using AirlineBookingSystem.Flights.Application.Commands;
using AirlineBookingSystem.Flights.Core.Interfaces.Repositories;
using MediatR;

namespace AirlineBookingSystem.Flights.Application.Handlers;

public class DeleteFlightHandler : IRequestHandler<DeleteFlightCommand>
{
    private readonly IFlightRepository _repository;

    public DeleteFlightHandler(IFlightRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteFlightAsync(request.Id);
    }
}
