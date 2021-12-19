using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider _ordersProvider;

        public OrdersController(IOrderProvider ordersProvider)
        {
            _ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            return Ok(await _ordersProvider.GetOrdersAsync(customerId));
        }
    }
}
