using Domain.Exceptions;
using Domain.Services.Bus.Messages;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services.Bus.Publishers.Strategies
{
    public class CreditCardBus : BaseStrategyPublisherBus, IStrategyPublisherBus
    {
        public CreditCardBus(IConnection connection, ILogger<CreditCardBus> logger) : base(connection, logger, routingKey: "creditcard") { }

        public bool IsMatch(BusMessage message)
        {
            return message is CreditCardMessage;
        }

        public bool Publish(BusMessage message)
        {
            if (!IsMatch(message)) throw new IncompatiblePublisherBusException(nameof(CreditCardMessage));

            var content = JsonSerializer.Serialize(message); 

            try
            {
                Connect();

                var body = Encoding.UTF8.GetBytes(content);

                _channel.BasicPublish(exchange: _exchange, routingKey: _routingKey, basicProperties: GetQueueProperties(), body: body);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when trying to publish message to Exchange: {_exchange}. Body: {content}. ErrorMessage: {ex.Message}");
            }

            return false;
        }

        private IBasicProperties GetQueueProperties()
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            return properties;
        }
    }
}
