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
        public Task<CreateOrderModel> UpdateOrderAsync(CreateOrderModel request, int id);
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
            if (!await _context.Orders.AnyAsync(x => x.Id == createOrder.UserId))
            {
                var orderEntity = new OrderEntity
                {
                    // I entityn = I CreateOrdermodel
                    UserId = createOrder.UserId,
                    UserName = $"{createOrder.UserFirstName} {createOrder.UserLastName}",
                    Address = $"{createOrder.StreetAddress} {createOrder.ZipCode} {createOrder.City}",
                    OrderDate = DateTime.Now,
                    DueDate = DateTime.Now,
                    OrderStatus = "Ok"
                };

                var orderRows = new List<OrderRowEntity>();
                foreach (var item in createOrder.OrderRows)
                    orderRows.Add(new OrderRowEntity
                    {
                        OrderId = orderEntity.Id,
                        ArticleNumber = item.ArticleNumber,
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        Price = item.ProductPrice,
                    });

                orderEntity.OrderRows = orderRows;

                _context.Orders.Add(orderEntity);
                await _context.SaveChangesAsync();
            }

            return null!;
        }
        public async Task<CreateOrderModel> UpdateOrderAsync(CreateOrderModel request, int id)
        {
            var orderEntity = await _context.Orders.FirstOrDefaultAsync(x => x.UserId == id);
            if (orderEntity != null)
            {
                orderEntity.UserName = $"{request.UserFirstName} {request.UserLastName}";
                orderEntity.Address = $"{request.StreetAddress} {request.ZipCode} {request.City}";
                orderEntity.OrderDate = DateTime.Now;
                orderEntity.OrderStatus = " ";

                _context.Entry(orderEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

               // return new CreateOrderModel(orderEntity.UserName, orderEntity.Address, orderEntity.OrderDate, orderEntity.OrderStatus);

            }

            return null!;
        }
        /*public async Task<IActionResult> UpdateOrderAsync(CreateOrderModel order, int id)
        {
            var orderEntity = await _context.Orders.Include(x => x.OrderRows).FirstOrDefaultAsync(x => x.Id == id);
            if (orderEntity == null)
            {
                return new NotFoundObjectResult("");
            }
        }*/
    }
}
