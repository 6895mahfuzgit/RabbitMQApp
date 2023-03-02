using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json.Serialization;

public class Program
{


    public static class Config
    {
        public static string RABBIT_MQ_URI = "amqp://guest:guest@localhost:5672";
        public static string QUEUE_NAME = "demo-queue";
    }

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