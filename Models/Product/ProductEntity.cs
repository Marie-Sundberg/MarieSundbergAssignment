using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarieSundbergAssignment.Models.Product
{
    public class ProductEntity
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string ArticleNumber { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        // Den här gör så att en produkt kan en kategori
        public virtual CategoryEntity Category { get; set; } = null!;
    }
}
