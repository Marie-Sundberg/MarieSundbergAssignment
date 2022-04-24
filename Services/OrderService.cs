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
        Task<OrderEntity> CreateOrderAsync(CreateOrderModel createOrder);
        Task<CreateOrderModel> UpdateOrderAsync(CreateOrderModel request, int id);
        Task<IEnumerable<OrderEntity>> GetAllOrdersAsync();
        Task<bool> DeleteOrderAsync(int id);
    }





    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _context;

        public OrderService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<OrderEntity> CreateOrderAsync(CreateOrderModel createOrder)
        {
            var orderEntity = new OrderEntity
            {
                // I entityn = I CreateOrdermodel
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
            return orderEntity;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity != null)
            {
                orderEntity.OrderStatus = "Makulerad";

                _context.Entry(orderEntity).State = EntityState.Modified;
                //_context.Orders.Remove(orderEntity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<OrderEntity>> GetAllOrdersAsync() => await _context.Orders.Where(x => x.OrderStatus != "Makulerad").Include(x => x.OrderRows).ToListAsync();


        // KOLLA DENNA, FUNKAR INTE RIKTIGT
        public async Task<CreateOrderModel> UpdateOrderAsync(CreateOrderModel request, int id)
        {
            var orderEntity = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
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

            return new CreateOrderModel();
        }
    }
}
