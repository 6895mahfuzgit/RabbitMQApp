using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StatisConfig;
using System.Text;

namespace RabbitMQConsumerApp
{
    public static class QueueConsumer
    {

        public static void Consume(IModel channel)
        {
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
}
