using Newtonsoft.Json;
using RabbitMQ.Client;
using StatisConfig;
using System.Text;

namespace RabbitMQProducerApp
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {

            Dictionary<string, object> ttl =new Dictionary<string, object>{
                { "x-message-ttl",30000}
            };

            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct,arguments: ttl);

            int count = 0;

            while (true)
            {
                var message = new { Name = "TestName", Message = "Test Message" };
                byte[]? body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject($"message {count}"));

                channel.BasicPublish("demo-direct-exchange", "account.init", null, body);
                count++;

                Thread.Sleep(1000);
            }
        }
    }
}
