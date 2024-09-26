using HiTech.RabbitMQ.Settings;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HiTech.RabbitMQ.Publisher
{
    public class MessagePublisher : IMessagePublisher
    {
        private bool _disposed = false;

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessagePublisher()
        {
            var factory = new ConnectionFactory
            {
                HostName = RabbitMQSettings.HostName,
                UserName = RabbitMQSettings.UserName,
                Password = RabbitMQSettings.Password,
                VirtualHost = RabbitMQSettings.VirtualHost
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Publish<T>(string queueName, T message)
        {
            _channel.QueueDeclare(queue: queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "",
                             routingKey: queueName,
                             basicProperties: null,
                             body: body);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Ngăn GC gọi finalizer
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Giải phóng các tài nguyên quản lý
                    _channel?.Close();
                    _connection?.Close();
                }

                // Giải phóng tài nguyên không quản lý (nếu có)

                _disposed = true;
            }
        }
    }
}
