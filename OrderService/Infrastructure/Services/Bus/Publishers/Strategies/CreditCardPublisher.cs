using Domain.Exceptions;
using MassTransit;
using Messages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Services.Bus.Publishers.Strategies
{
    public class CreditCardPublisher : BaseStrategyPublisherBus, IStrategyPublisherBus
    {
        public CreditCardPublisher(IBus bus, ILogger<CreditCardPublisher> logger) : base(bus, logger) { }

        public bool IsMatch(BusMessage message)
        {
            return message is CreditRequestedMessage;
        }

        public async Task<bool> Publish(BusMessage message)
        {
            if (message is not CreditRequestedMessage creditcardMessage) throw new IncompatiblePublisherBusException(nameof(CreditRequestedMessage));

            try
            {
                var endpoint = await _bus.GetPublishSendEndpoint<CreditRequestedMessage>();

                await endpoint.Send(creditcardMessage, context => context.Durable = true);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when trying to publish message {nameof(CreditRequestedMessage)}. Body: {JsonSerializer.Serialize(message)}. ErrorMessage: {ex.Message}");
            }

            return false;
        }
    }
}
