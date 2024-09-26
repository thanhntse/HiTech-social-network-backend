using RabbitMQ.Client;
using System.Text;

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
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Publish(string queueName, string message)
        {
            _channel.QueueDeclare(queue: queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

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
