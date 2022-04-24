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
        Task<Order> CreateOrderAsync(List<Product> shoppingcart, UserModel user);
        //Task<Order> CreateOrderAsync(int id, CreateOrderModel createOrder);
    }

    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _context;

        public OrderService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrderAsync(List<Product> shoppingcart, UserModel user)
        {
            //var orderEntity = await _context.Users.FindAsync(id);
                
                    var orderEntity = new OrderEntity
                    {
                        // I entityn = I Usermodel (createOrderModel)
                        UserName = $"{user.FirstName} {user.LastName}",
                        Address = $"{user.StreetAddress} {user.ZipCode} {user.City}",
                        OrderDate = DateTime.Now,
                    };

            var orderRows = new List<OrderRowEntity>();
            foreach (var item in shoppingcart)
                orderRows.Add(new OrderRowEntity
                {
                    OrderId = orderEntity.Id,
                    ArticleNumber = item.ArticleNumber,
                    ProductName = item.ProductName,
                });

            orderEntity.OrderRows = orderRows; 

            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();
            return null!;
        }
    }
}
