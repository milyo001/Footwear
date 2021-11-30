namespace Footwear.Data.Models
{
    using Footwear.Data.Dto;
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

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string Payment { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public List<BillingInformation> UserData { get; set; } = new List<BillingInformation>();

        [Required]
        public virtual ICollection<CartProduct> Products { get; set; } = new List<CartProduct>();


    }
}
