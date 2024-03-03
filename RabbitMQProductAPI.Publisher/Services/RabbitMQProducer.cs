using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQProductAPI.Publisher.Services
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            ConnectionFactory factory = new();
            factory.HostName = "localhost";

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("product", exclusive: false);
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange:"", routingKey:"product", body: body);
        }
    }
}
