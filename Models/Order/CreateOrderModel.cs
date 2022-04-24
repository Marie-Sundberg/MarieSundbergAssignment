
namespace MarieSundbergAssignment.Models.Order
{
    public class CreateOrderModel
    {

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; } = null!;
        public string UserFirstName { get; set; } = null!;
        public string UserLastName { get; set; } = null!;
        public string StreetAddress { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }


        public virtual List<OrderRowModel> OrderRows { get; set; } = null!;

    }

    public class OrderRowModel
    {
        public string ArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
