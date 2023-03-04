using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQProducerApp
{
    public static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel)
        {

            Dictionary<string, object> ttl = new Dictionary<string, object>{
                { "x-message-ttl",30000}
            };

            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout, arguments: ttl);

            int count = 0;

            while (true)
            {
                var message = new { Name = "TestName", Message = "Test Message" };
                byte[]? body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject($"message {count}"));


                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "new" } };

                channel.BasicPublish("demo-fanout-exchange", string.Empty, properties, body);
                count++;

                Thread.Sleep(1000);
            }
        }

    }
}