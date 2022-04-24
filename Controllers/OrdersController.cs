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
        public async Task<IActionResult> CreateOrder(CreateOrderModel orderModel, int id)
        {
            var user = await _service.CreateOrderAsync(orderModel, id);

            if (user != null)
            {
                return new OkObjectResult(user);
            }

            return new BadRequestResult();
        }

        //[HttpPut("{id}")]


    }
}
