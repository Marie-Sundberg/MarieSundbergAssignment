namespace MarieSundbergAssignment.Models.Product
{
    public class Product
    {
        public Product()
        {

        }
        public Product(int id, string articleNumber, string productName, string productDescription, decimal productPrice, string categoryName)
        {
            Id = id;
            ArticleNumber = articleNumber;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            CategoryName = categoryName;
        }

        public int Id { get; set; }
        public string ArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
