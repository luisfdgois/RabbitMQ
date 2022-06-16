using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Concrete
{
    public abstract class PublisherQueueBase : IDisposable
    {
        protected readonly ILogger<PublisherQueueBase> _logger;
        protected readonly IConnection _connection;
        protected IModel _channel;

        protected readonly string _queue;
        protected readonly string _routingKey;
        protected readonly string _exchange = "order-exchange";

        protected PublisherQueueBase(IConnection connection, ILogger<PublisherQueueBase> logger,
            string queue, string routingKey)
        {
            _connection = connection;
            _logger = logger;

            _queue = queue;
            _routingKey = routingKey;
        }

        protected void ConnectToRabbitMQ()
        {
            if (_channel is object && _channel.IsOpen)
                return;

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _exchange, type: "direct", durable: true, autoDelete: false);

            _channel.QueueDeclare(queue: _queue, durable: true, exclusive: false, autoDelete: false);

            _channel.QueueBind(queue: _queue, exchange: _exchange, routingKey: _routingKey);
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
        }
    }
}
