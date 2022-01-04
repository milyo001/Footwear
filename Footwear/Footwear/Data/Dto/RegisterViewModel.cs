
namespace Footwear.Data.Dto
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {

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

