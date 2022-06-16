using Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Contracts;
using Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Settings;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Concrete
{
    public class CreditCardQueue : PublisherQueueBase, IPublisherQueue
    {
        public CreditCardQueue(IConnection connection, ILogger<CreditCardQueue> logger) :
            base(connection, logger, queue: "creditcard-queue", routingKey: "creditcard-routingkey")
        { }

        public AvailableQueue Queue { get { return AvailableQueue.CreditCard; } }

        public bool Publish(string jsonContent)
        {
            ConnectToRabbitMQ();

            try
            {
                var body = Encoding.UTF8.GetBytes(jsonContent);

                _channel.BasicPublish(exchange: _exchange, routingKey: _routingKey, basicProperties: GetQueueProperties(), body: body);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to publish message to the {_queue}. Body: {jsonContent}. ErrorMessage: {ex.Message}");
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
