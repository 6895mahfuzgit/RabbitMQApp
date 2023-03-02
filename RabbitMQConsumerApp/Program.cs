using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, e) =>
        {
            byte[] body = e.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };

        channel.BasicConsume(Config.QUEUE_NAME, true, consumer);

        Console.ReadLine();
    }
}