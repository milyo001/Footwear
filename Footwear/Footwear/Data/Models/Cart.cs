namespace Footwear.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey(nameof(ProductImage))]
        public int UserId { get; set; }

        public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();


    }
}
