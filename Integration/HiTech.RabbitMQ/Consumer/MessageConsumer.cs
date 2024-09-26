using HiTech.RabbitMQ.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HiTech.RabbitMQ.Consumer
{
    public class MessageConsumer : IMessageConsumer
    {
        private bool _disposed = false;

        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageConsumer()
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

        public void CreateConsumer(string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            _channel.QueueDeclare(queue: queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += receivedHandler;

            _channel.BasicConsume(queue: queueName,
                                  autoAck: true,
                                  consumer: consumer);
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
