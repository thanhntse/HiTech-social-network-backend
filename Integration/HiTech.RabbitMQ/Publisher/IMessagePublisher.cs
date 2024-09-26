namespace HiTech.RabbitMQ.Publisher
{
    public interface IMessagePublisher : IDisposable
    {
        void Publish<T>(string queueName, T message);
    }
}
