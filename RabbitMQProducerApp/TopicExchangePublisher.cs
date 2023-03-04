using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQProducerApp
{
    public static class TopicExchangePublisher
    {
        public static void Publish(IModel channel)
        {

            Dictionary<string, object> ttl = new Dictionary<string, object>{
                { "x-message-ttl",30000}
            };

            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic, arguments: ttl);

            int count = 0;

            while (true)
            {
                var message = new { Name = "TestName", Message = "Test Message" };
                byte[]? body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject($"message {count}"));

                channel.BasicPublish("demo-topic-exchange", "account.pro", null, body);
                count++;

                Thread.Sleep(1000);
            }
        }

    }
}