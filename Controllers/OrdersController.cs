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
        public async Task<IActionResult> CreateOrder(List<Product> shoppingcart, UserModel user)
        {
            var item = await _service.CreateOrderAsync(shoppingcart, user);
            if (item != null)
            {
                return new OkObjectResult(item);
            }

            return new BadRequestResult();
        }


    }
}
