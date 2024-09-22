namespace HiTech.RabbitMQ.Consumer
{
    public interface IMessageConsumer<TMessage, TProcessMethod>
        where TMessage : class, new()
        where TProcessMethod : class, new()
    {
    }
}
