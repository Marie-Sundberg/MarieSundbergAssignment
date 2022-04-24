namespace MarieSundbergAssignment.Models.Order
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderTime { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductName { get; set; } = null!;
        public int UserId { get; set; }
    }
}
