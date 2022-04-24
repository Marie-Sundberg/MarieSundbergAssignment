using MarieSundbergAssignment.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarieSundbergAssignment.Models.Order
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string OrderStatus { get; set; } = null!;

        // kan hämta ut alla orderrader på en order
        public virtual ICollection<OrderRowEntity> OrderRows { get; set; } = null!;
        
    }

    //vilka produkter ordern har
    public class OrderRowEntity
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string ArticleNumber { get; set; } = null!;

        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        // kan hämta ordern utifrån en orderrad (med hjälp av include)
        public OrderEntity Order { get; set; } = null!;

    }
}
