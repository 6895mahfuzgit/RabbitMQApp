using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibraryApp.Models;

namespace OrderServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private IPublishEndpoint _endpoint;

        public OrderController(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }


        [HttpPost("SaveOrder")]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            _endpoint.Publish<Order>(order);
            return Ok();
        }

    }
}
