using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQConsumerApp;
using StatisConfig;
using System.Text;

public class Program
{

    public static void Main(string[] args)
    {

        ConnectionFactory factory = new ConnectionFactory { Uri = new Uri(Config.RABBIT_MQ_URI) };
        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        QueueConsumer.Consume(channel);
    }
}