using Newtonsoft.Json;
using RabbitMQ.Client;
using StatisConfig;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        ConnectionFactory factory = new ConnectionFactory { Uri = new Uri(Config.RABBIT_MQ_URI) };
        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.QueueDeclare(Config.QUEUE_NAME,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        var message = new { Name = "TestName", Message = "Test Message" };
        byte[]? body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

        channel.BasicPublish("", Config.QUEUE_NAME, null, body);
    }
}