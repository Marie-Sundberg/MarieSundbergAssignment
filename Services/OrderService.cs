using MarieSundbergAssignment.Models;
using MarieSundbergAssignment.Models.Order;
using MarieSundbergAssignment.Models.Product;
using MarieSundbergAssignment.Models.User;
using Microsoft.EntityFrameworkCore;

namespace MarieSundbergAssignment.Services
{
    public interface IOrderService
    {
        // skicka tillbaka en order = <Order>
        // vill stoppa in en hel order (Order order)
        Task<Order> CreateOrderAsync(CreateOrderModel createOrder);
        //Task<Order> CreateOrderAsync(int id, CreateOrderModel createOrder);
        //Task<> UpdateOrderAsync(CreateOrderModel order, int id);
    }

    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _context;

        public OrderService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrderAsync(CreateOrderModel createOrder)
        {
            if (!await _context.Users.AnyAsync(x => x.Id == createOrder.UserId))
            {
                var orderEntity = new OrderEntity
                {
                    // I entityn = I Usermodel (createOrderModel)
                    UserId = createOrder.UserId,
                    UserName = $"{createOrder.UserFirstName} {createOrder.UserLastName}",
                    Address = $"{createOrder.StreetAddress} {createOrder.ZipCode} {createOrder.City}",
                    OrderDate = DateTime.Now,
                    DueDate = DateTime.Now,
                    OrderStatus = "Ok"
                };
            }


            var orderRows = new List<OrderRowEntity>();
            foreach (var item in createOrder)
                orderRows.Add(new OrderRowEntity
                {
                    OrderId = orderEntity.Id,
                    ArticleNumber = item.ArticleNumber,
                    ProductName = item.ProductName,
                    Price = item.ProductPrice,
                    Quantity = item.
                });

            orderEntity.OrderRows = orderRows; 

            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();
            return null!;
        }
        public async Task<IActionResult> UpdateOrderAsync(CreateOrderModel order, int id)
        {
            var orderEntity = await _context.Orders.Include(x => x.OrderRows).FirstOrDefaultAsync(x => x.Id == id);
            if (orderEntity == null)
            {
                return new NotFoundObjectResult("");
            }
        }
    }
}
