using RabbitMQ.Client.Events;

namespace HiTech.RabbitMQ.Consumer
{
    public interface IMessageConsumer : IDisposable
    {
        void CreateConsumer(string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler);
    }
}
