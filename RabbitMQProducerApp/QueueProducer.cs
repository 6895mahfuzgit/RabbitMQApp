using Newtonsoft.Json;
using RabbitMQ.Client;
using StatisConfig;
using System.Text;

namespace RabbitMQProducerApp
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)
        {

            channel.QueueDeclare(Config.QUEUE_NAME,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            int count = 0;

            while (true)
            {
                var message = new { Name = "TestName", Message = "Test Message" };
                byte[]? body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject($"message {count}"));

                channel.BasicPublish("", Config.QUEUE_NAME, null, body);
                count++;

                Thread.Sleep(1000);
            }
        }


    }
}
