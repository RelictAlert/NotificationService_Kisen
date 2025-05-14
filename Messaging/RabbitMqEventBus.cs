using NotificationService_Kisen.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NotificationService.Messaging
{
    public class RabbitMqEventBus : IEventBus, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMqEventBus(IConfiguration cfg)
        {
            var factory = new ConnectionFactory
            {
                HostName = cfg["RabbitMq:Host"],
                UserName = cfg["RabbitMq:User"],
                Password = cfg["RabbitMq:Pass"]
            };

            _connection = factory
                .CreateConnectionAsync()
                .GetAwaiter()
                .GetResult();
            _channel = _connection
                .CreateChannelAsync()
                .GetAwaiter()
                .GetResult();

            _channel.ExchangeDeclareAsync(
                    exchange: "alerts.exchange",
                    type: ExchangeType.Topic,
                    durable: true)
                .GetAwaiter()
                .GetResult();
        }

        public void Publish<T>(T @event, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            var props = new BasicProperties();

            _channel.BasicPublishAsync(
                    exchange: "alerts.exchange",
                    routingKey: routingKey,
                    mandatory: false,
                    basicProperties: props,
                    body: body)
                .GetAwaiter()
                .GetResult();
        }

        public void Subscribe<T>(string routingKey, Func<T, Task> handler)
        {
            var queueDeclareResult = _channel
                .QueueDeclareAsync()
                .GetAwaiter()
                .GetResult();
            var queueName = queueDeclareResult.QueueName;

            _channel.QueueBindAsync(
                    queueName,
                    "alerts.exchange",
                    routingKey,
                    arguments: null)
                .GetAwaiter()
                .GetResult();

            _channel.QueueBindAsync(
                queueName,
                "alerts.exchange",
                routingKey,
                arguments: null)
                .GetAwaiter()
                .GetResult();


            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (sender, ea) =>
            {
                var msg = Encoding.UTF8.GetString(ea.Body.ToArray());
                var @event = JsonSerializer.Deserialize<T>(msg);
                if (@event != null)
                {
                    await handler(@event);
                }
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsumeAsync(
                    queue: queueName,
                    autoAck: false,
                    consumer: consumer)
                .GetAwaiter()
                .GetResult();
        }

        public void Dispose()
        {
            _channel.CloseAsync().GetAwaiter().GetResult();
            _connection.CloseAsync().GetAwaiter().GetResult();
        }
    }
}
