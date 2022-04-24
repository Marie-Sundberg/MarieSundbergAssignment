using MarieSundbergAssignment.Models.Order;
using MarieSundbergAssignment.Models.Product;
using MarieSundbergAssignment.Models.User;
using MarieSundbergAssignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarieSundbergAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }




        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderModel orderModel)
        {
            var order = await _service.CreateOrderAsync(orderModel);

            if (order != null)
            {
                return new OkObjectResult(order);
            }

            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return new OkObjectResult(await _service.GetAllOrdersAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(CreateOrderModel order, int id)
        {
            var item = await _service.UpdateOrderAsync(order, id);
            if (item != null)
            {
                return new OkObjectResult(item);
            }

            return new BadRequestResult();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (await _service.DeleteOrderAsync(id))
            {
                return new OkResult();
            }

            return new BadRequestResult();
        }



    }
}
