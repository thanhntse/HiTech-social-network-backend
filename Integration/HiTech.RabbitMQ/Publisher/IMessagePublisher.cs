namespace HiTech.RabbitMQ.Publisher
{
    public interface IMessagePublisher : IDisposable
    {
        void Publish(string queueName, string message);
    }
}
