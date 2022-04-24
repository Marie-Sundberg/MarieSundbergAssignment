using MarieSundbergAssignment.Models;
using MarieSundbergAssignment.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace MarieSundbergAssignment.Services
{
    public interface IProductService
    {

        //skapa en produkt CREATE (C)
        public Task<Product> CreateProductAsync(ProductModel request);

        //hämtar alla produkter READ (R)
        public Task<IEnumerable<Product>> GetAllProductsAsync();

        // uppdatera en produkt UPDATE (U)
        public Task<ProductModel> UpdateProductAsync(int id, ProductModel request);

        // tar bort produkt DELETE (D)
        public Task<bool> DeleteProductAsync(int id);

    }







    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context;

        public ProductService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(ProductModel request)
        {
            // finns en produkt med det här namnet
            if (!await _context.Products.AnyAsync(x => x.Name == request.ProductName))
            {
                // finns product med den här kategorin
                var categoryEntity = await _context.Categories.FirstOrDefaultAsync(x => x.Name == request.CategoryName);
                if (categoryEntity == null)
                {
                    categoryEntity = new CategoryEntity { Name = request.CategoryName };
                    _context.Categories.Add(categoryEntity);
                    await _context.SaveChangesAsync();
                }
                var productEntity = new ProductEntity
                {
                    ArticleNumber = request.ArticleNumber,
                    Name = request.ProductName,
                    Description = request.ProductDescription,
                    Price = request.ProductPrice,
                    CategoryId = categoryEntity.Id
                };

                _context.Products.Add(productEntity);
                await _context.SaveChangesAsync();

                // skicka tillbaka en ny produkt
                return new Product
                {
                    Id = productEntity.Id,
                    ArticleNumber = productEntity.ArticleNumber,
                    ProductName = productEntity.Name,
                    ProductDescription = productEntity.Description,
                    ProductPrice = productEntity.Price,
                    CategoryName = productEntity.Category.Name
                };
            }
            return null!;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity != null)
            {
                _context.Products.Remove(productEntity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var items = new List<Product>();
            foreach (var item in await _context.Products.Include(x => x.Category).ToListAsync())
                items.Add(new Product
                {
                    Id = item.Id,
                    ArticleNumber = item.ArticleNumber,
                    ProductName = item.Name,
                    ProductDescription = item.Description,
                    ProductPrice = item.Price,
                    CategoryName = item.Category.Name
                });

            return items;
        }

        public async Task<ProductModel> UpdateProductAsync(int id, ProductModel request)
        {
            var productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (productEntity != null)
            {
                productEntity.ArticleNumber = request.ArticleNumber;
                productEntity.Name = request.ProductName;
                productEntity.Price = request.ProductPrice;
                productEntity.Description = request.ProductDescription;
                productEntity.Category.Name = request.CategoryName;
                _context.Entry(productEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new ProductModel(productEntity.ArticleNumber, productEntity.Name, productEntity.Description, productEntity.Price, productEntity.Category.Name);

            }

            return null!;

        }
    }
}
