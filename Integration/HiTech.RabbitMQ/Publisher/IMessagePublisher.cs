namespace HiTech.RabbitMQ.Publisher
{
    public interface IMessagePublisher<TMessage>
        where TMessage : class, new()
    {
        void Publish(string queueName, TMessage message);
    }
}
