using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using StatisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsumerApp
{
    public static class TopicExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic);
            channel.QueueDeclare(Config.QUEUE_NAME,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

            channel.QueueBind(Config.QUEUE_NAME, "demo-topic-exchange", "account.*");

            channel.BasicQos(0, 10, false);

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
}
