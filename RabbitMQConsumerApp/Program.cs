
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Unicode;

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

        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, e) =>
        {
            byte[] body = e.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };

        channel.BasicConsume(Config.QUEUE_NAME,true,consumer);

        Console.ReadLine();


    }
}