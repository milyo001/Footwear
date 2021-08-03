namespace Footwear.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();
    }
}
