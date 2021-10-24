namespace Footwear.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        public Address Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public Cart Cart {get; set;}


    }
}
