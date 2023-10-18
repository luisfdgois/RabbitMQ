using Domain.Exceptions;
using Domain.Services.Bus.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Services.Bus.Publishers.Strategies
{
    public class CreditCardBus : BaseStrategyPublisherBus, IStrategyPublisherBus
    {
        public CreditCardBus(IBus bus, ILogger<CreditCardBus> logger) : base(bus, logger) { }

        public bool IsMatch(BusMessage message)
        {
            return message is CreditCardMessage;
        }

        public async Task<bool> Publish(BusMessage message)
        {
            if (message is not CreditCardMessage creditcardMessage) throw new IncompatiblePublisherBusException(nameof(CreditCardMessage));

            try
            {
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{_queue}?"));

                await endpoint.Send(creditcardMessage, context => context.Durable = true);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when trying to publish message to Queue: {_queue}. Body: {JsonSerializer.Serialize(message)}. ErrorMessage: {ex.Message}");
            }

            return false;
        }
    }
}
