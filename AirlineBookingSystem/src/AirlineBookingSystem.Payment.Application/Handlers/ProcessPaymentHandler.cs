using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Payments.Application.Commands;
using AirlineBookingSystem.Payments.Core.Entities;
using AirlineBookingSystem.Payments.Core.Interfaces.Repositories;
using MassTransit;
using MediatR;

namespace AirlineBookingSystem.Payments.Application.Handlers;

public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, Guid>
{
    private readonly IPaymentRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProcessPaymentHandler(IPaymentRepository repository, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            BookingId = request.BookingId,
            Amount = request.Amount,
            PaymentDate = DateTime.UtcNow,
        };

        await _repository.ProcessPaymentAync(payment);

        // Publish PaymentProcessedEvent
        await _publishEndpoint.Publish(new PaymentProcessedEvent(payment.Id, payment.BookingId, payment.Amount, payment.PaymentDate), cancellationToken);

        return payment.Id;
    }
}
