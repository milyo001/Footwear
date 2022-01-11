using System.ComponentModel.DataAnnotations;

namespace Footwear.Data.Dto
{
    public class PasswordViewModel
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string  NewPassword { get; set; }

        [Required]
        public string  ConfirmPassword { get; set; }
    }
}
