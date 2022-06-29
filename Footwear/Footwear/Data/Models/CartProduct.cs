

namespace Footwear.Data.Models
{
    using Footwear.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class CartProduct 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Range(0.1, 10000)]
        public double Price { get; set; }

        [Required, MaxLength(500)]
        public string Details { get; set; }

        [ForeignKey("ProductImage")]
        public int ImageId { get; set; }

        public virtual ProductImage ProductImage { get; set; }

        public Gender Gender { get; set; }

        public ProductType ProductType { get; set; }
        public string ImageUrl { get; set; }

        public int? Size { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public int CartId { get; set; }

        public bool IsOrdered { get; set; }

        public string OrderId { get; set; }

    }
}
