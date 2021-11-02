
using System.ComponentModel.DataAnnotations;

namespace Footwear.Data.Dto
{

    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

}

