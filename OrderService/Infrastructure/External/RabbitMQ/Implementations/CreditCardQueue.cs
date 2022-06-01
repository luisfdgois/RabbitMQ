using Infrastructure.External.RabbitMQ.Contracts;
using Infrastructure.External.RabbitMQ.Settings;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.External.RabbitMQ.Implementations
{
    public class CreditCardQueue : QueueBase, IQueue
    {
        public CreditCardQueue(ConnectionFactory connectionFactory, ILogger<CreditCardQueue> logger) :
            base(connectionFactory, logger, queue: "creditcard-queue", routingKey: "creditcard-routingkey")
        { }

        public bool IsMatch(AvailableQueue availableQueue)
        {
            return availableQueue is AvailableQueue.CreditCard;
        }

        public bool Publish(string jsonContent)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(jsonContent);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;

                _channel.BasicPublish(exchange: _exchange, routingKey: _routingKey, basicProperties: properties, body: body);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to publish message to the {_queue}. Body: {jsonContent}. ErrorMessage: {ex.Message}");
            }

            return false;
        }
    }
}
