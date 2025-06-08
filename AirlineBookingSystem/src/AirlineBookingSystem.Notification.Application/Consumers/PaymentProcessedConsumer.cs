using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Notifications.Application.Commands;
using MassTransit;
using MediatR;

namespace AirlineBookingSystem.Notifications.Application.Consumers;

public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
{
    private readonly IMediator _mediator;

    public PaymentProcessedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        var paymentProcessedEvent = context.Message;

        var message = $"Payment of {paymentProcessedEvent.Amount:C} for booking {paymentProcessedEvent.BookingId} has been processed successfully on {paymentProcessedEvent.PaymentDate}.";

        var command = new SendNotificationCommand("someone@example.com", message, "Email");

        await _mediator.Send(command);
    }
}
