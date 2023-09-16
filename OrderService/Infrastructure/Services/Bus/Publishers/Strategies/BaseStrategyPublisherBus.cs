using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Infrastructure.Services.Bus.Publishers.Strategies
{
    public abstract class BaseStrategyPublisherBus : IDisposable
    {
        protected readonly ILogger<BaseStrategyPublisherBus> _logger;
        protected readonly IConnection _connection;
        protected IModel _channel;

        protected readonly string _routingKey;
        protected readonly string _exchange = "order-exchange";

        protected BaseStrategyPublisherBus(IConnection connection, ILogger<BaseStrategyPublisherBus> logger, string routingKey)
        {
            _connection = connection;
            _logger = logger;

            _routingKey = routingKey;
        }

        protected void Connect()
        {
            if (_channel is object && _channel.IsOpen)
                return;

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Direct);
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
        }
    }
}
