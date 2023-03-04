using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQProducerApp;
using StatisConfig;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        ConnectionFactory factory = new ConnectionFactory { Uri = new Uri(Config.RABBIT_MQ_URI) };
        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        // QueueProducer.Publish(channel);
        // DirectExchangePublisher.Publish(channel);
        TopicExchangePublisher.Publish(channel);

    }
}