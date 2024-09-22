using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HiTech.RabbitMQ.Consumer
{
    public class MessageConsumer<TMessage, TProcessMethod> : IMessageConsumer<TMessage, TProcessMethod>
        where TMessage : class, new()
        where TProcessMethod : class, new()
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly Func<TProcessMethod, TMessage, Task> _processMethod;

        public MessageConsumer(string hostName, string queueName, Func<TProcessMethod, TMessage, Task> processMethod)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _processMethod = processMethod;

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnMessageReceived;
            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private async void OnMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var message = JsonSerializer.Deserialize<TMessage>(json);

            // Gọi phương thức xử lý
            await _processMethod(default(TProcessMethod), message);
        }
    }
}
