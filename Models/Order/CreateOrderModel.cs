namespace MarieSundbergAssignment.Models.Order
{
    public class CreateOrderModel
    {

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string StreetAddress { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }

    }
}
