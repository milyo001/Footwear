
namespace Footwear.Data.Dto
{
    using Footwear.Data.Models;
    using System.Collections.Generic;

    public class RegisterViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }

}

