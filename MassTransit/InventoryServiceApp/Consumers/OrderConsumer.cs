using MassTransit;
using SharedLibraryApp.Models;

namespace InventoryServiceApp.Consumers
{
    public class OrderConsumer : IConsumer<Order>
    {
        private ILogger<OrderConsumer> _logger;

        public OrderConsumer(ILogger<OrderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Order> context)
        {
            _logger.LogInformation($"Got new message {context.Message.Name}");
        }
    }
}
