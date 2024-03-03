namespace RabbitMQProductAPI.Publisher.Services
{
    public interface IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message);
    }
}
