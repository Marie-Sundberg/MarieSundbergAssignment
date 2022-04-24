using System.ComponentModel.DataAnnotations;

namespace MarieSundbergAssignment.Models.Product
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;


        // Den här gör så att en kategori kan ha flera produkter (med <ICollection>)
        public virtual ICollection<ProductEntity> Products { get; set; } = null!;
    }
}
