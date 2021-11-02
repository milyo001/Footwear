
namespace Footwear.Data.Dto
{
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }


    }

}

