namespace CreditCard.RabbitMQ.Settings
{
    public record RabbitMQProperties
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public string VirtualHost { get; init; }
        public string Port { get; init; }
        public string HostName { get; init; }

        public string Uri { get { return $"amqp://{UserName}:{Password}@{HostName}:{Port}/{VirtualHost}"; } }
    }

}
