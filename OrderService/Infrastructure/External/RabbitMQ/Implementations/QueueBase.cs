using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Infrastructure.External.RabbitMQ.Implementations
{
    public abstract class QueueBase : IDisposable
    {
        protected readonly ConnectionFactory _connectionFactory;
        protected readonly ILogger<QueueBase> _logger;
        protected IConnection _connection;
        protected IModel _channel;

        protected readonly string _queue;
        protected readonly string _routingKey;
        protected readonly string _exchange = "order-exchange";

        protected QueueBase(ConnectionFactory connectionFactory, ILogger<QueueBase> logger, string queue, string routingKey)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;

            _queue = queue;
            _routingKey = routingKey;

            ConnectToRabbitMQ();
        }

        private void ConnectToRabbitMQ()
        {
            if (_connection is null || !_connection.IsOpen)
                _connection = _connectionFactory.CreateConnection();

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

            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
