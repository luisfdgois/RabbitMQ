using RabbitMQ.Client;
using System.Text;

namespace CreditCard.Service.RabbitMQ.Publisher
{
    public class QueuePublisher : IQueuePublisher
    {
        private readonly ILogger<QueuePublisher> _logger;
        private readonly IConnection _connection;
        private IModel _channel;

        private readonly string _queue = "paymentprocessed-queue";

        public QueuePublisher(ILogger<QueuePublisher> logger, IConnection connection)
        {
            _logger = logger;
            _connection = connection;

            ConnectToRabbitMQ();
        }

        public bool Publish(string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;

                _channel.BasicPublish(exchange: "", routingKey: _queue, basicProperties: properties, body: body);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to publish message to the {_queue}. Body: {message}. ErrorMessage: {ex.Message}");
            }

            return false;
        }

        private void ConnectToRabbitMQ()
        {
            if (_channel is object && _channel.IsOpen)
                return;

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queue, durable: true, exclusive: false, autoDelete: false);
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
        }
    }
}
