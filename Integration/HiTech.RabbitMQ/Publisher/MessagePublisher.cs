using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HiTech.RabbitMQ.Publisher
{
    public class MessagePublisher<TMessage> : IMessagePublisher<TMessage> 
        where TMessage : class, new()
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessagePublisher(string hostName, string queueName)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void Publish(string queueName, TMessage messageObject)
        {
            var json = JsonSerializer.Serialize(messageObject);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
