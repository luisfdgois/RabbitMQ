using RabbitMQ.Client;
using System.Text;

namespace CreditCard.Bus.Publisher
{
    public class QueuePublisher : IQueuePublisher
    {
        private readonly IConnection _connection;
        private IModel _channel;

        private readonly string _queue = "paymentprocessed-queue";
        private readonly string _exchange = "paymentprocessed-exchange";

        // public QueuePublisher(IConnection connection)
        // {
        //     _connection = connection;

        //     ConnectToRabbitMQ();
        // }

        public bool Publish(string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(exchange: _exchange, routingKey: _queue, basicProperties: GetQueueProperties(), body: body);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error trying to publish message to the {_queue}. Body: {message}. ErrorMessage: {ex.Message}");
            }

            return false;
        }

        private void ConnectToRabbitMQ()
        {
            if (_channel is object && _channel.IsOpen)
                return;

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _exchange, type: "fanout", autoDelete: false);

            _channel.QueueDeclare(queue: _queue, durable: true, exclusive: false, autoDelete: false);

            _channel.QueueBind(queue: _queue, exchange: _exchange, routingKey: _queue);
        }

        private IBasicProperties GetQueueProperties()
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            return properties;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
        }
    }
}
