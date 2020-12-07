namespace Footwear.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Order
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string CreatedOn { get; set; }

        public string OrderStatus { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
