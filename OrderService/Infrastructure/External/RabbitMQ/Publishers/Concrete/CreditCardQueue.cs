using Infrastructure.External.RabbitMQ.Publishers.Contracts;
using Infrastructure.External.RabbitMQ.Publishers.Settings;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.External.RabbitMQ.Publishers.Concrete
{
    public class CreditCardQueue : PublisherQueueBase, IPublisherQueue
    {
        public CreditCardQueue(IConnection connection, ILogger<CreditCardQueue> logger) :
            base(connection, logger, queue: "creditcard-queue", routingKey: "creditcard-routingkey")
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
