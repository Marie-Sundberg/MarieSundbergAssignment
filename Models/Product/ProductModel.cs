namespace MarieSundbergAssignment.Models.Product
{
    public class ProductModel
    {
        public ProductModel()
        {

        }
        public ProductModel(string articleNumber, string productName, string productDescription, decimal productPrice, string categoryName)
        {
            ArticleNumber = articleNumber;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            CategoryName = categoryName;
        }

        public string ArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
