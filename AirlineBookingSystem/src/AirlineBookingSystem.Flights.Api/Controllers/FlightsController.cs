using AirlineBookingSystem.Flights.Application.Commands;
using AirlineBookingSystem.Flights.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.Flights.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FlightsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetFlights()
    {
        var flights = await _mediator.Send(new GetAllFlightsQuery());

        return Ok(flights);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFlight([FromBody] CreateFlightCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid flight data.");
        }

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetFlights), new { id }, command);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlightBy(Guid id)
    {
        await _mediator.Send(new DeleteFlightCommand(id));

        return NoContent();
    }
}
